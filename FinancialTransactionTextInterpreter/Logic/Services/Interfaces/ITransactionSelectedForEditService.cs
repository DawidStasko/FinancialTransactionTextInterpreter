using FinancialTransactionTextInterpreter.Model;

namespace FinancialTransactionTextInterpreter.Logic.Services.Interfaces;
public interface ITransactionSelectedForEditService
{
					public event Action<InscribedTransaction>? TransactionSelectedForEdit;

					public void InformAboutTransactionToEdit(InscribedTransaction transaction);
}
