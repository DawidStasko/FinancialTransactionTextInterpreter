using WpfFinancialTransactionPromptInterpreter.Logic.Services.Interfaces;
using WpfFinancialTransactionPromptInterpreter.Model;
namespace WpfFinancialTransactionPromptInterpreter.Logic.Services;
public class NewTransactionCreatedService : INewTransactionCreatedService
{
					public event Action<InscribedTransaction> NewTransactionCreated;

					public void InformAboutNewTransaction(InscribedTransaction transaction)
					{
										NewTransactionCreated?.Invoke(transaction);
					}
}
