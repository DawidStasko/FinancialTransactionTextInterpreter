using CommunityToolkit.Mvvm.ComponentModel;
using FinancialTransactionTextInterpreter.Model.Interfaces;
using System.Windows;
using Wpf.Ui;
using WpfFinancialTransactionPromptInterpreter.ViewModels;

namespace FinancialTransactionTextInterpreter.ViewModels;

public partial class MainWindowVM : ObservableObject
{
					private readonly IConfig _config;
					private readonly ISnackbarService _snackbarService;

					[ObservableProperty]
					private bool _isHelpBoxVisible = true;

					[ObservableProperty]
					private Visibility _helpBoxVisibility = Visibility.Hidden;

					[ObservableProperty]
					private Visibility _inscribedTransactionsListVisibility = Visibility.Visible;

					public PromptInputVM PromptInputVM { get; }
					public InscribedTransactionsListVM InscribedTransactionsListVM { get; }
					public HelpBoxVM HelpBoxVM { get; }

					public MainWindowVM(PromptInputVM promptInputVM, InscribedTransactionsListVM inscribedTransactionsListVM, IConfig config, ISnackbarService snackbarService, HelpBoxVM helpBoxVM)
					{
										ArgumentNullException.ThrowIfNull(promptInputVM);
										ArgumentNullException.ThrowIfNull(inscribedTransactionsListVM);
										ArgumentNullException.ThrowIfNull(config);
										ArgumentNullException.ThrowIfNull(snackbarService);
										ArgumentNullException.ThrowIfNull(helpBoxVM);

										PromptInputVM = promptInputVM;
										InscribedTransactionsListVM = inscribedTransactionsListVM;
										HelpBoxVM = helpBoxVM;
										_config = config;
										_snackbarService = snackbarService;
					}

					partial void OnIsHelpBoxVisibleChanged(bool value)
					{
										if (value)
										{
															HelpBoxVisibility = Visibility.Visible;
															InscribedTransactionsListVisibility = Visibility.Hidden;
										}
										else
										{
															HelpBoxVisibility = Visibility.Hidden;
															InscribedTransactionsListVisibility = Visibility.Visible;
										}
					}

					internal void SetNewFileName(string fileName) => _config.FinancialDataFullyQualifiedFileName = fileName;
					internal string GetFilePath() => _config.FinancialDataFullyQualifiedFileName;
}
