using FinancialTransactionTextInterpreter.Model;

namespace FinancialTransactionTextInterpreter.Logic.Services.Interfaces;

public interface ITransactionSaverService
{
					IList<Result<Transaction>> SaveTransactions(IList<Transaction> transactions);
}