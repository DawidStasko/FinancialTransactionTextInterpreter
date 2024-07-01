﻿using CommunityToolkit.Mvvm.ComponentModel;
using FinancialTransactionTextInterpreter.Model.Interfaces;
using System.Windows;
using Wpf.Ui;

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
					private Visibility _inscribedTransactionsListVisibility = Visibility.Visible;

					public TextInputVM TextInputVM { get; }
					public InscribedTransactionsListVM InscribedTransactionsListVM { get; }
					public LanguageSelectorVM LanguageSelectorVM { get; }

					public MainWindowVM(TextInputVM textInputVM, InscribedTransactionsListVM inscribedTransactionsListVM, IConfig config, ISnackbarService snackbarService, LanguageSelectorVM languageSelectorVM)
					{
										ArgumentNullException.ThrowIfNull(textInputVM);
										ArgumentNullException.ThrowIfNull(inscribedTransactionsListVM);
										ArgumentNullException.ThrowIfNull(languageSelectorVM);
										ArgumentNullException.ThrowIfNull(config);
										ArgumentNullException.ThrowIfNull(snackbarService);

										TextInputVM = textInputVM;
										InscribedTransactionsListVM = inscribedTransactionsListVM;
										_config = config;
										_snackbarService = snackbarService;
										LanguageSelectorVM = languageSelectorVM;
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
