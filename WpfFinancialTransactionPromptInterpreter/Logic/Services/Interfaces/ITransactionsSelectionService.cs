using WpfFinancialTransactionPromptInterpreter.Model;

namespace WpfFinancialTransactionPromptInterpreter.Logic.Services.Interfaces;

public interface ITransactionsSelectionService
{
					InscribedTransaction? SelectedTransaction { get; set; }
					event EventHandler? SelectionChanged;
}