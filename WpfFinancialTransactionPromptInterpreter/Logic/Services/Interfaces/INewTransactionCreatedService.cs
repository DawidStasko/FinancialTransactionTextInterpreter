using WpfFinancialTransactionPromptInterpreter.Model;

namespace WpfFinancialTransactionPromptInterpreter.Logic.Services.Interfaces;
public interface INewTransactionCreatedService
{
					event Action<InscribedTransaction> NewTransactionCreated;

					void InformAboutNewTransaction(InscribedTransaction transaction);
}