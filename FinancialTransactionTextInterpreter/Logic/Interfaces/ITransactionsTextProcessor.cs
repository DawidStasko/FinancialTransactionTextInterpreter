using FinancialTransactionTextInterpreter.Model;

namespace FinancialTransactionTextInterpreter.Logic.Interfaces;
public interface ITransactionsTextProcessor
{
					(IList<InscribedTransaction> successfullyProcessed, IList<InscribedTransaction> unsuccessfullyProcessed) ProcessMultipleTransactions(IEnumerable<InscribedTransaction> inscribedTransactions);
}