using WpfFinancialTransactionPromptInterpreter.Model;

namespace WpfFinancialTransactionPromptInterpreter.Logic.Interfaces;
public interface ITransactionsTextProcessor
{
					(IList<InscribedTransaction> successfullyProcessed, IList<InscribedTransaction> unsuccessfullyProcessed) ProcessMultipleTransactions(IEnumerable<InscribedTransaction> inscribedTransactions);
}