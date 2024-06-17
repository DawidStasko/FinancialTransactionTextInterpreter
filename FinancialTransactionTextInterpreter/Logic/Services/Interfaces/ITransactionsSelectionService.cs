using FinancialTransactionTextInterpreter.Model;

namespace FinancialTransactionTextInterpreter.Logic.Services.Interfaces;

public interface ITransactionsSelectionService
{
					InscribedTransaction? SelectedTransaction { get; set; }
					event EventHandler? SelectionChanged;
}