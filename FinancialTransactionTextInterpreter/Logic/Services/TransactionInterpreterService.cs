using FinancialTransactionTextInterpreter.Logic.Services.Interfaces;
using FinancialTransactionTextInterpreter.Model;
using System.Globalization;
using System.Text;

namespace FinancialTransactionTextInterpreter.Logic.Services;

public class TransactionInterpreterService : ITransactionInterpreterService
{
					public Result<IList<Transaction>> ProcessTransactionText(InscribedTransaction transactionText)
					{

										string[] transactionTextWords = transactionText.Text?.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries) ?? [];

										if (transactionTextWords.Length == 0)
															return new Result<IList<Transaction>>() { ErrorMessages = ["Transaction text is empty"] };

										int numberOfAccounts = transactionTextWords.Count(w => w.StartsWith('$'));
										if (numberOfAccounts != 1 && numberOfAccounts != 2)
															return new Result<IList<Transaction>>() { ErrorMessages = ["Transaction text should contain at least one and no more than two accounts"] };

										Result<IList<Transaction>>? result = null;

										if (numberOfAccounts == 1)
															result = ProcessExternalTransaction(transactionTextWords);

										if (numberOfAccounts == 2)
															result = ProcessTransferTransactions(transactionTextWords);

										return result ?? new Result<IList<Transaction>>() { ErrorMessages = ["Unexpected error occurred when processing transaction."] };
					}

					private Result<IList<Transaction>>? ProcessExternalTransaction(string[] transactionTextWords)
					{
										List<string> errors = new();
										Transaction transaction = new();
										string actualCategory = "";
										IList<string> tagsList = [];

										for (int i = 0; i < transactionTextWords.Length; i++)
										{
															string word = transactionTextWords[i];
															switch (word[0])
															{
																				case '&':
																									bool result = DateOnly.TryParseExact(word.Substring(1), "dd-MM-yyyy", out DateOnly date);
																									if (!result)
																														errors.Add("Date format should be dd-MM-yyyy");
																									else
																														transaction.Date = date;
																									break;
																				case '$':
																									transaction.Account = word.Substring(1);
																									break;
																				case '@':
																									transaction.Contractor = word.Substring(1);
																									break;
																				case '#':
																									actualCategory = word.Substring(1);
																									break;
																				case '!':
																									tagsList.Add(word.Substring(1));
																									break;
																				default:
																									if (string.IsNullOrEmpty(actualCategory))
																									{
																														errors.Add("Items need to have category written before them.");
																														actualCategory = "MissingCategory";
																									}

																									bool isPrice = TryParseDecimal(word, out _);
																									if (isPrice)
																									{
																														Item item = new();
																														item.Name = actualCategory;
																														item.Price = CalculatePrice(transactionTextWords, ref i);
																														item.Category = actualCategory;
																														transaction.Items.Add(item);
																									}
																									else
																									{
																														Item item = new();
																														item.Name = BuildName(transactionTextWords, ref i);
																														item.Price = CalculatePrice(transactionTextWords, ref i);
																														item.Category = actualCategory;
																														transaction.Items.Add(item);
																									}
																									break;
															}
										}

										if (errors.Any())
															return new Result<IList<Transaction>>() { ErrorMessages = errors };

										return new Result<IList<Transaction>> { Value = [transaction] };
					}

					private decimal CalculatePrice(string[] transactionTextWords, ref int actualIndex)
					{
										decimal totalPrice = decimal.Zero;
										for (; actualIndex < transactionTextWords.Length; actualIndex++)
										{
															bool isPrice = TryParseDecimal(transactionTextWords[actualIndex], out decimal partialPrice);
															if (isPrice)
															{
																				if (transactionTextWords[actualIndex][0] == '+')
																				{
																									totalPrice += partialPrice;
																				}
																				else
																				{
																									totalPrice += (-Math.Abs(partialPrice));
																				}
															}
															else
															{
																				actualIndex--;
																				break;
															}
										}
										return totalPrice;
					}

					private string BuildName(IList<string> transactionTextWords, ref int actualIndex)
					{
										StringBuilder stringBuilder = new();

										for (; actualIndex < transactionTextWords.Count; actualIndex++)
										{
															bool isPrice = TryParseDecimal(transactionTextWords[actualIndex], out _);
															if (!isPrice)
																				stringBuilder.Append(transactionTextWords[actualIndex] + " ");
															else
																				break;
										}
										return stringBuilder.ToString();
					}

					private Result<IList<Transaction>>? ProcessTransferTransactions(IList<string> transactionTextWords)
					{
										List<string> errors = new();
										string? sourceAccount = null;
										string? targetAccount = null;
										List<decimal> items = new();
										IList<string> tagsList = [];
										DateOnly date = DateOnly.FromDateTime(DateTime.Now);

										for (int i = 0; i < transactionTextWords.Count; i++)
										{
															string word = transactionTextWords[i];
															switch (word[0])
															{
																				case '&':
																									bool result = DateOnly.TryParseExact(word.Substring(1), "dd-MM-yyyy", out date);
																									if (!result)
																														errors.Add("Date format should be dd-MM-yyyy");
																									break;
																				case '$':
																									sourceAccount = word.Substring(1);
																									if (transactionTextWords[i + 1] != ">")
																														errors.Add("'>' need to show cashflow from source account to target account and need to be right between two accounts.");

																									if (transactionTextWords[i + 2][0] != '$')
																														errors.Add("Proper transfer transactions need to define source and target accounts in this maner: $source > $target");
																									else
																									{
																														targetAccount = transactionTextWords[i + 2].Substring(1);
																														i += 2;
																									}
																									break;
																				case '@':
																									errors.Add("Contractor not allowed in transfer transaction.");
																									break;
																				case '#':
																									errors.Add("Categories not allowed in transfer transaction.");
																									break;
																				case '!':
																									tagsList.Add(word.Substring(1));
																									break;
																				default:
																									bool isPrice = TryParseDecimal(word, out decimal price);
																									if (!isPrice)
																									{
																														errors.Add("Details names not allowed in transfer transaction.");
																														break;
																									}
																									items.Add(price);
																									break;
															}

										}

										if (errors.Any())
															return new Result<IList<Transaction>>() { ErrorMessages = errors };

										Transaction outgoingTransaction = new()
										{
															Date = date,
															Account = sourceAccount,
															Contractor = targetAccount,
															Items = [new Item()
															{
																			Name = "Transfer",
																			Price = -items.Sum(),
																			Category = "TransferOut",
																			Tags = tagsList.ToArray()
															}]
										};

										Transaction incomingTransaction = new()
										{
															Date = date,
															Account = targetAccount,
															Contractor = sourceAccount,
															Items = [new Item()
															{
																			Name = "Transfer",
																			Price = items.Sum(),
																			Category = "TransferOut",
																			Tags = tagsList.ToArray()
															}]
										};

										return new Result<IList<Transaction>> { Value = [outgoingTransaction, incomingTransaction] };
					}

					private bool TryParseDecimal(string value, out decimal result)
					{
										string normalizedValue = value.Replace(",", ".");
										return decimal.TryParse(normalizedValue, CultureInfo.InvariantCulture, out result);
					}

					private enum TransactionType
					{
										Transfer,
										ExternalTransaction
					}
}
