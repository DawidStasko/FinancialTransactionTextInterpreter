using FinancialTransactionTextInterpreter.Model;

namespace FinancialTransactionTextInterpreter.Logic.Services.Interfaces;
public interface ITransactionCreatedService
{
					public event Action<InscribedTransaction>? TransactionCreated;

					public void InformAboutTransactionCreated(InscribedTransaction transaction);
}
