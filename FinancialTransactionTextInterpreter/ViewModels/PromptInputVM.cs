using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinancialTransactionTextInterpreter.Logic.Services.Interfaces;
using FinancialTransactionTextInterpreter.Model;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;

namespace WpfFinancialTransactionPromptInterpreter.ViewModels;

public partial class PromptInputVM : ObservableObject
{
					private readonly ITransactionsSelectionService _selectionService;
					private readonly INewTransactionCreatedService _newTransactionCreatedService;
					private readonly ISuggestionsService _suggestionsService;
					private readonly ILogger<PromptInputVM> _logger;
					private bool _showSuggestions;
					private string _textInput;
					private ObservableCollection<string> _suggestionsList;
					private string _actualWord;

					public string TextInput
					{
										get { return _textInput; }
										set
										{
															_textInput = value;
															OnPropertyChanged();
										}
					}

					public ObservableCollection<string> SuggestionsList
					{
										get { return _suggestionsList; }
										set
										{
															_suggestionsList = value;
															OnPropertyChanged();
										}
					}

					public string ActualWord
					{
										get { return _actualWord; }
										set
										{
															_actualWord = value;
															OnPropertyChanged();
										}
					}

					public PromptInputVM(ITransactionsSelectionService selectionService,
									INewTransactionCreatedService newTransactionCreatedService,
									ISuggestionsService suggestionsService,
									ILogger<PromptInputVM> logger)
					{
										_selectionService = selectionService;
										_selectionService.SelectionChanged += (s, e) =>
										{
															TextInput = _selectionService.SelectedTransaction?.Text ?? string.Empty;
										};

										_newTransactionCreatedService = newTransactionCreatedService;

										PropertyChanged += (s, e) =>
										{
															if (e.PropertyName == nameof(ActualWord))
															{
																				if (string.IsNullOrEmpty(ActualWord))
																				{
																									SuggestionsList = [];
																									return;
																				}
																				SuggestionsList = new ObservableCollection<string>(_suggestionsService?.GetSuggestions(ActualWord) ?? []);
															}
										};

										_suggestionsService = suggestionsService;
										_logger = logger;
					}

					[RelayCommand]
					private void ProcessText()
					{
										if (_selectionService.SelectedTransaction == null)
										{
															InscribedTransaction transaction = new(_textInput);
															_logger.LogInformation($"Creating new transaction {transaction.Id}");
															_newTransactionCreatedService.InformAboutNewTransaction(new InscribedTransaction(_textInput));
										}
										else
										{
															_selectionService.SelectedTransaction.Text = _textInput;
										}

										ClearText();
					}

					[RelayCommand]
					private void ClearText()
					{
										TextInput = string.Empty;
										_selectionService.SelectedTransaction = null;
					}
}
