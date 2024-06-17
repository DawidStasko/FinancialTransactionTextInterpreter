using FinancialTransactionTextInterpreter.Model;

namespace FinancialTransactionTextInterpreter.Logic.Services.Interfaces;

public interface ITransactionInterpreterService
{
					Result<IList<Transaction>> ProcessTransactionText(InscribedTransaction transactionText);
}