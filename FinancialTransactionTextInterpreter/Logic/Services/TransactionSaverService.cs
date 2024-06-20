using FinancialTransactionTextInterpreter.Logic.ExternalInterfaces;
using FinancialTransactionTextInterpreter.Logic.Services.Interfaces;
using FinancialTransactionTextInterpreter.Model;

namespace FinancialTransactionTextInterpreter.Logic.Services;
internal class TransactionSaverService : ITransactionSaverService
{
					private ITransactionsRepository _transactionsRepository;

					public TransactionSaverService(ITransactionsRepository transactionsRepository)
					{
										_transactionsRepository = transactionsRepository;
					}

					public IList<Result<Transaction>> SaveTransactions(IList<Transaction> transactions)
					{
										IList<Result<Transaction>> results = new List<Result<Transaction>>();
										foreach (Transaction transaction in transactions)
										{
															try
															{
																				results.Add(_transactionsRepository.Save(transaction));
															}
															catch (Exception ex)
															{
																				results.Add(new Result<Transaction>() { Value = transaction, ErrorMessages = new List<string>() { ex.Message } });
															}
										}

										return results;
					}
}
