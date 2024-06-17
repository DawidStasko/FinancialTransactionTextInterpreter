using FinancialTransactionTextInterpreter.Logic.Services.Interfaces;
using FinancialTransactionTextInterpreter.Model;

namespace FinancialTransactionTextInterpreter.Logic.Services;

public class TransactionsSelectionService : ITransactionsSelectionService
{
					private InscribedTransaction? _selectedTransaction;

					public InscribedTransaction? SelectedTransaction
					{
										get { return _selectedTransaction; }
										set
										{
															_selectedTransaction = value;
															OnSelectionChanged();
										}
					}

					public event EventHandler? SelectionChanged;

					protected virtual void OnSelectionChanged()
					{
										SelectionChanged?.Invoke(this, EventArgs.Empty);
					}
}
