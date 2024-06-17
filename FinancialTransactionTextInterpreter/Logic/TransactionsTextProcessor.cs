using FinancialTransactionTextInterpreter.Logic.ExternalInterfaces;
using FinancialTransactionTextInterpreter.Logic.Interfaces;
using FinancialTransactionTextInterpreter.Logic.Services.Interfaces;
using FinancialTransactionTextInterpreter.Model;
using Microsoft.Extensions.Logging;

namespace FinancialTransactionTextInterpreter.Logic;

public class TransactionsTextProcessor : ITransactionsTextProcessor
{
					private readonly ITransactionsRepository _transactionsRepository;
					private readonly ITransactionInterpreterService _transactionInterpreterService;
					private readonly ILogger<TransactionsTextProcessor> _logger;

					public TransactionsTextProcessor(
										ITransactionsRepository transactionsRepository,
										ILogger<TransactionsTextProcessor> logger,
										ITransactionInterpreterService transactionInterpreterService)
					{
										_transactionsRepository = transactionsRepository;
										_logger = logger;
										_transactionInterpreterService = transactionInterpreterService;
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
																				Result<IList<Transaction>> interpretationResult = _transactionInterpreterService.ProcessTransactionText(inscribedTransaction);
																				if (interpretationResult.IsSuccess)
																				{
																									foreach (Transaction transaction in interpretationResult.Value)
																									{
																														_transactionsRepository.Save(transaction);
																									}
																				}
																				else
																				{
																									unsuccessfullyProcessed.Add(inscribedTransaction);
																									string errorMessage = string.Join(Environment.NewLine, interpretationResult.ErrorMessages);
																									_logger.LogError("Error while processing transaction: {transaction}.\n {errorMessage}", inscribedTransaction.Text, errorMessage);
																				}
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

										}
										catch (Exception e)
										{
															_logger.LogError(e, "Error while processing transaction: {transaction}", inscribedTransaction.Text);
										}
					}

}