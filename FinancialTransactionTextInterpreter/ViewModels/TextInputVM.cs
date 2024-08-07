﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinancialTransactionTextInterpreter.Logic.Services.Interfaces;
using FinancialTransactionTextInterpreter.Model;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;

namespace FinancialTransactionTextInterpreter.ViewModels;

public partial class TextInputVM : ObservableObject
{
					private InscribedTransaction? _actualTransaction = null;

					private readonly ITransactionCreatedService _transactionCreatedService;
					private readonly ITransactionSelectedForEditService _transactionSelectedForEditService;
					private readonly ITransactionInterpreterService _transactionInterpreterService;
					private readonly ISuggestionsService _suggestionsService;
					private readonly ILogger<TextInputVM> _logger;

					[ObservableProperty]
					private string _textInput = "";

					[ObservableProperty]
					private ObservableCollection<string> _suggestionsList = new();

					[ObservableProperty]
					private string _actualWord = "";

					public TextInputVM(
									ISuggestionsService suggestionsService,
									ILogger<TextInputVM> logger,
									ITransactionInterpreterService transactionInterpreterService,
									ITransactionCreatedService transactionCreatedService,
									ITransactionSelectedForEditService transactionSelectedForEditService)
					{
										ArgumentNullException.ThrowIfNull(suggestionsService);
										ArgumentNullException.ThrowIfNull(logger);
										ArgumentNullException.ThrowIfNull(transactionSelectedForEditService);
										ArgumentNullException.ThrowIfNull(transactionCreatedService);
										ArgumentNullException.ThrowIfNull(transactionInterpreterService);

										_suggestionsService = suggestionsService;
										_logger = logger;
										_transactionInterpreterService = transactionInterpreterService;
										_transactionCreatedService = transactionCreatedService;
										_transactionSelectedForEditService = transactionSelectedForEditService;

										_transactionSelectedForEditService.TransactionSelectedForEdit += OnTransactionSelectedForEdit;
					}

					private void OnTransactionSelectedForEdit(InscribedTransaction transaction)
					{
										_actualTransaction = transaction;
										TextInput = _actualTransaction.Text;
					}

					[RelayCommand]
					private void ProcessText()
					{
										if (string.IsNullOrWhiteSpace(TextInput))
										{
															_logger.LogWarning("No transaction to process or empty text.");
															return;
										}

										InscribedTransaction transaction = _actualTransaction ?? new InscribedTransaction("");
										transaction.Text = TextInput;

										transaction.ProcessingResult = _transactionInterpreterService.ProcessTransactionText(transaction);
										_logger.LogInformation($"Processing transaction {transaction.Id}");

										if (_actualTransaction == null)
															_transactionCreatedService.InformAboutTransactionCreated(transaction);

										ClearText();
					}

					[RelayCommand]
					private void ClearText()
					{
										TextInput = string.Empty;
										_actualTransaction = null;
					}

					partial void OnActualWordChanged(string? oldValue, string newValue)
					{
										if (string.IsNullOrWhiteSpace(ActualWord))
										{
															SuggestionsList = [];
															return;
										}

										SuggestionsList = new ObservableCollection<string>(_suggestionsService?.GetSuggestions(ActualWord) ?? []);
					}
}
