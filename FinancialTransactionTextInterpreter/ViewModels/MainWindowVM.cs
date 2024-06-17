using FinancialTransactionTextInterpreter.Model.Interfaces;
using WpfFinancialTransactionPromptInterpreter.ViewModels;

namespace FinancialTransactionTextInterpreter.ViewModels;

public class MainWindowVM
{
					private readonly IConfig _config;

					public PromptInputVM PromptInputVM { get; }
					public InscribedTransactionsListVM InscribedTransactionsListVM { get; }


					public MainWindowVM(PromptInputVM promptInputVM, InscribedTransactionsListVM inscribedTransactionsListVM, IConfig config)
					{
										PromptInputVM = promptInputVM;
										InscribedTransactionsListVM = inscribedTransactionsListVM;
										_config = config;
					}

					internal void SetNewFileName(string fileName) => _config.FinancialDataFullyQualifiedFileName = fileName;
}
