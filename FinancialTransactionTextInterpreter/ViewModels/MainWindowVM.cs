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
					private bool _isHelpBoxVisible = false;

					[ObservableProperty]
					private Visibility _helpBoxVisibility = Visibility.Hidden;

					[ObservableProperty]
					private Visibility _inscribedTransactionsListVisibility = Visibility.Hidden;

					public PromptInputVM PromptInputVM { get; }
					public InscribedTransactionsListVM InscribedTransactionsListVM { get; }

					public MainWindowVM(PromptInputVM promptInputVM, InscribedTransactionsListVM inscribedTransactionsListVM, IConfig config, ISnackbarService snackbarService)
					{
										PromptInputVM = promptInputVM;
										InscribedTransactionsListVM = inscribedTransactionsListVM;
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
}
