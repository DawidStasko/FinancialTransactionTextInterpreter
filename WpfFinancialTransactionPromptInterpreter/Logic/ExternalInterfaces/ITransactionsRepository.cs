using WpfFinancialTransactionPromptInterpreter.Model;

namespace WpfFinancialTransactionPromptInterpreter.Logic.ExternalInterfaces;

public interface ITransactionsRepository
{
					public void Save(Transaction transaction);
}