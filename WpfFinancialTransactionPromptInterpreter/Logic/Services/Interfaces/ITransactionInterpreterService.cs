using WpfFinancialTransactionPromptInterpreter.Model;

namespace WpfFinancialTransactionPromptInterpreter.Logic.Services.Interfaces;

public interface ITransactionInterpreterService
{
					Result<IList<Transaction>> ProcessTransactionText(InscribedTransaction transactionText);
}