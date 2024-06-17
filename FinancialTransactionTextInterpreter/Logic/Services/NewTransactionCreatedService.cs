using FinancialTransactionTextInterpreter.Logic.Services.Interfaces;
using FinancialTransactionTextInterpreter.Model;

namespace FinancialTransactionTextInterpreter.Logic.Services;
public class NewTransactionCreatedService : INewTransactionCreatedService
{
					public event Action<InscribedTransaction> NewTransactionCreated;

					public void InformAboutNewTransaction(InscribedTransaction transaction)
					{
										NewTransactionCreated?.Invoke(transaction);
					}
}
