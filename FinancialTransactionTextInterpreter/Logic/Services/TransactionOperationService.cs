using FinancialTransactionTextInterpreter.Logic.Services.Interfaces;
using FinancialTransactionTextInterpreter.Model;

namespace FinancialTransactionTextInterpreter.Logic.Services;
public class TransactionOperationService : ITransactionCreatedService, ITransactionSelectedForEditService
{
					public event Action<InscribedTransaction>? TransactionCreated;
					public event Action<InscribedTransaction>? TransactionSelectedForEdit;

					public void InformAboutTransactionCreated(InscribedTransaction transaction)
					{
										if (string.IsNullOrWhiteSpace(transaction.Text))
															return;
										TransactionCreated?.Invoke(transaction);
					}

					public void InformAboutTransactionToEdit(InscribedTransaction transaction)
					{
										TransactionSelectedForEdit?.Invoke(transaction);
					}
}
