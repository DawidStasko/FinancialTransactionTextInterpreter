using FinancialTransactionTextInterpreter.Model;

namespace FinancialTransactionTextInterpreter.Logic.Services.Interfaces;
public interface INewTransactionCreatedService
{
					event Action<InscribedTransaction> NewTransactionCreated;

					void InformAboutNewTransaction(InscribedTransaction transaction);
}