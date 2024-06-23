using FinancialTransactionTextInterpreter.Model;

namespace FinancialTransactionTextInterpreter.Logic.InfrastructureInterfaces;

public interface ITransactionsRepository
{
					public Result<Transaction> Save(Transaction transaction);
}