using FinancialTransactionTextInterpreter.Model;

namespace FinancialTransactionTextInterpreter.Logic.ExternalInterfaces;

public interface ITransactionsRepository
{
					public void Save(Transaction transaction);
}