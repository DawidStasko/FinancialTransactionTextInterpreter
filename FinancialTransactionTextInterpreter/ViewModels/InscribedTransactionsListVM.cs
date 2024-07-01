using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinancialTransactionTextInterpreter.Logic.Services.Interfaces;
using FinancialTransactionTextInterpreter.Model;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace FinancialTransactionTextInterpreter.ViewModels;

public partial class InscribedTransactionsListVM : ObservableObject
{
					private readonly ITransactionCreatedService _transactionCreatedOrUpdatedService;
					private readonly ITransactionSelectedForEditService _transactionSelectedForEditService;
					private readonly ITransactionInterpreterService _transactionInterpreterService;
					private readonly ITransactionSaverService _transactionSaverService;
					private readonly ISnackbarService _snackbarService;
					private readonly ILogger<InscribedTransactionsListVM> _logger;

					[ObservableProperty]
					private ObservableCollection<InscribedTransaction> _inscribedTransactions;

					[ObservableProperty]
					private InscribedTransaction? _selectedItem;

					public InscribedTransactionsListVM(
										ISnackbarService snackbarService,
										ILogger<InscribedTransactionsListVM> logger,
										ITransactionInterpreterService transactionInterpreterService,
										ITransactionSaverService transactionSaverService,
										ITransactionCreatedService transactionCreatedOrUpdatedService,
										ITransactionSelectedForEditService transactionSelectedForEditService)
					{
										ArgumentNullException.ThrowIfNull(logger);
										ArgumentNullException.ThrowIfNull(snackbarService);
										ArgumentNullException.ThrowIfNull(transactionInterpreterService);
										ArgumentNullException.ThrowIfNull(transactionSaverService);
										ArgumentNullException.ThrowIfNull(transactionCreatedOrUpdatedService);
										ArgumentNullException.ThrowIfNull(transactionSelectedForEditService);

										_snackbarService = snackbarService;
										_logger = logger;
										_transactionInterpreterService = transactionInterpreterService;
										_transactionSaverService = transactionSaverService;
										_transactionCreatedOrUpdatedService = transactionCreatedOrUpdatedService;
										_transactionSelectedForEditService = transactionSelectedForEditService;

										InscribedTransactions = new ObservableCollection<InscribedTransaction>();

										_transactionCreatedOrUpdatedService.TransactionCreated += OnTransactionCreated;

										CreateTestingData();
					}

					private void OnTransactionCreated(InscribedTransaction transaction)
					{
										if (transaction is not null)
										{
															InscribedTransactions.Insert(0, transaction);
										}
					}

					[RelayCommand]
					private void Edit(InscribedTransaction selectedTransaction)
					{
										if (selectedTransaction != null)
															_transactionSelectedForEditService.InformAboutTransactionToEdit(selectedTransaction);

					}

					[RelayCommand]
					private void Delete()
					{
										if (SelectedItem != null)
															InscribedTransactions.Remove(SelectedItem);
					}

					[RelayCommand]
					private void ProcessTransactions()
					{
										foreach (InscribedTransaction t in InscribedTransactions)
										{
															t.ProcessingResult = _transactionInterpreterService.ProcessTransactionText(t);
										}

										List<Transaction> successfullyProcessed = InscribedTransactions.Where(x => !x.HasErrors).SelectMany(x => x.ProcessingResult.Value!).ToList();
										IList<Result<Transaction>> results = _transactionSaverService.SaveTransactions(successfullyProcessed);
										_snackbarService.Show("Transactions saved", $"Successfully processed {results.Where(x => x.IsSuccess).Count()} out of {successfullyProcessed.Count}.", ControlAppearance.Primary, null, TimeSpan.FromSeconds(20));
										List<InscribedTransaction> newCollection = InscribedTransactions.Where(x => x.HasErrors).ToList();
										newCollection.AddRange(results.Where(x => !x.IsSuccess).Select(x => new InscribedTransaction(x.Value?.ToString() ?? "")
										{
															ProcessingResult = new Result<IList<Transaction>>() { Value = [x.Value!], ErrorMessages = x.ErrorMessages }
										}));
										InscribedTransactions.Clear();
										InscribedTransactions = new(newCollection);
					}

					private void CreateTestingData()
					{
#if DEBUG
										InscribedTransactions = new ObservableCollection<InscribedTransaction>()
										{
														new($"&{DateTime.Now.AddDays(-2).ToString("dd-MM-yyyy")} $PKO #Jedzenie To Powinno Miec Wartosc Minus1 -6 +5 Tenutaj Minus8 -8 #Transport -302,02 @StacjaDokowania"),
														new($"&{DateTime.Now.AddDays(-2).ToString("dd-MM-yyyy")} $PKO To Powinno Miec Wartosc Minus1 -6 +5 Tenutaj Minus8 -8 #Transport -302,02 @StacjaDokowania"),
														new($"&{DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy")} $PKO > $Santander 30 40,1"),
														new($"&{DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy")} $PKO > $Santander @Żaba 30 40,1"),
										};

										foreach (InscribedTransaction t in InscribedTransactions)
										{
															t.ProcessingResult = _transactionInterpreterService.ProcessTransactionText(t);
										}
#endif
					}
}
