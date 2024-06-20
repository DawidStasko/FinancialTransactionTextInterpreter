using FinancialTransactionTextInterpreter.Model;

namespace FinancialTransactionTextInterpreter.Logic.ExternalInterfaces;

public interface ITransactionsRepository
{
					public Result<Transaction> Save(Transaction transaction);
}