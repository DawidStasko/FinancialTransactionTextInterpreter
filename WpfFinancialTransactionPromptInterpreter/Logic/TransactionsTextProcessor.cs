using Microsoft.Extensions.Logging;
using System.Globalization;
using WpfFinancialTransactionPromptInterpreter.Logic.ExternalInterfaces;
using WpfFinancialTransactionPromptInterpreter.Logic.Interfaces;
using WpfFinancialTransactionPromptInterpreter.Model;

namespace WpfFinancialTransactionPromptInterpreter.Logic;

public class TransactionsTextProcessor : ITransactionsTextProcessor
{
					private readonly ITransactionsRepository _transactionsRepository;
					private readonly ILogger<TransactionsTextProcessor> _logger;
					public TransactionsTextProcessor(ITransactionsRepository transactionsRepository, ILogger<TransactionsTextProcessor> logger)
					{
										_transactionsRepository = transactionsRepository;
										_logger = logger;
					}

					public (IList<InscribedTransaction> successfullyProcessed, IList<InscribedTransaction> unsuccessfullyProcessed) ProcessMultipleTransactions(IEnumerable<InscribedTransaction> inscribedTransactions)
					{
										IList<InscribedTransaction> successfullyProcessed = new List<InscribedTransaction>();
										IList<InscribedTransaction> unsuccessfullyProcessed = new List<InscribedTransaction>();
										foreach (InscribedTransaction inscribedTransaction in inscribedTransactions)
										{
															try
															{
																				if (string.IsNullOrEmpty(inscribedTransaction?.Text))
																									continue;
																				Transaction transaction = InterpretText(inscribedTransaction.Text);
																				_transactionsRepository.Save(transaction);
															}
															catch (Exception e)
															{
																				unsuccessfullyProcessed.Add(inscribedTransaction);
																				_logger.LogError(e, "Error while processing transaction: {transaction}", inscribedTransaction.Text);
															}
															successfullyProcessed.Add(inscribedTransaction);
										}
										return (successfullyProcessed, unsuccessfullyProcessed);
					}

					public void ProcessSingleTransaction(InscribedTransaction inscribedTransaction)
					{
										try
										{
															if (string.IsNullOrEmpty(inscribedTransaction?.Text))
																				return;
															Transaction transaction = InterpretText(inscribedTransaction.Text);
															_transactionsRepository.Save(transaction);
										}
										catch (Exception e)
										{
															_logger.LogError(e, "Error while processing transaction: {transaction}", inscribedTransaction.Text);
										}
					}

					/// <summary>
					/// This text should have this format: 
					/// &dateOfTransaction $Account @contractor #category1 item1 price item2 price #category2 item3 price !tag item4 price 
					/// where ! @ # $ & are information about data stored in this string. All items after category belongs only to this category. 
					/// Tags should be assigned to first following item.
					/// </summary>
					/// <param name="text"></param>
					private Transaction InterpretText(string text)
					{

										IList<string> words = [.. text.Split(' ')];
										Transaction transaction = new();
										string actualCategory = "";
										IList<string> tagsList = [];
										for (int i = 0; i < words.Count; i++)
										{
															string word = words[i];
															switch (word[0])
															{
																				case '&':
																									transaction.Date = DateTime.Parse(word.Substring(1));
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
																									//TODO here take all words starting from actual word, untill you find price or something with special starter. 
																									string itemName = FindItemName(words, ref i);
																									Item item = new();
																									item.Name = itemName;
																									item.Category = actualCategory;
																									item.Tags = tagsList.ToArray();
																									string priceString = words[i + 1];
																									bool success = decimal.TryParse(priceString, CultureInfo.InvariantCulture, out decimal price);
																									if (success)
																									{
																														item.Price = price;
																														i++;
																									}
																									else
																									{
																														item.Price = null;
																									}
																									transaction.Items.Add(item);
																									break;
															}
										}
										return transaction;
					}

					private string FindItemName(IList<string> words, ref int index)
					{
										string itemName = "";

										for (int i = index; i < words.Count; i++)
										{
															char firstLetter = words[i][0];
															if (firstLetter == '$' || firstLetter == '@' || firstLetter == '#' || firstLetter == '!' || firstLetter == '&')
															{
																				break;
															}

															bool success = decimal.TryParse(words[i], CultureInfo.InvariantCulture, out decimal price);
															if (success)
															{
																				break;
															}

															itemName += words[i] + " ";
															index = i;
										}

										return itemName;
					}
}