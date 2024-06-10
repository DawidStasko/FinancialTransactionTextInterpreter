using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Wpf.Ui;
using WpfFinancialTransactionPromptInterpreter.Logic.Interfaces;
using WpfFinancialTransactionPromptInterpreter.Logic.Services.Interfaces;
using WpfFinancialTransactionPromptInterpreter.Model;

namespace WpfFinancialTransactionPromptInterpreter.ViewModels;

public partial class InscribedTransactionsListVM : ObservableObject
{
					private readonly INewTransactionCreatedService _newTransactionCreatedService;
					private readonly ITransactionsSelectionService? _selectionService;
					private readonly ITransactionsTextProcessor? _transactionsTextProcessor;
					private readonly ISnackbarService _snackbarService;

					private ObservableCollection<InscribedTransaction> _inscribedTransactions = new();

					public ObservableCollection<InscribedTransaction> InscribedTransactions
					{
										get { return _inscribedTransactions; }
										set
										{
															_inscribedTransactions = value;
															OnPropertyChanged();
										}
					}

					public InscribedTransactionsListVM(ITransactionsSelectionService selectionService,
										ITransactionsTextProcessor? transactionsTextProcessor,
										INewTransactionCreatedService newTransactionCreatedService,
										ISnackbarService snackbarService)
					{
										_selectionService = selectionService;
										_transactionsTextProcessor = transactionsTextProcessor;
										_newTransactionCreatedService = newTransactionCreatedService;
										_newTransactionCreatedService.NewTransactionCreated += (transaction) =>
										{
															InscribedTransactions.Insert(0, transaction);
										};
										InscribedTransactions = new ObservableCollection<InscribedTransaction>();

#if DEBUG
										InscribedTransactions = new ObservableCollection<InscribedTransaction>()
										{
															new("&2024-03-09 $Account3 @Contractor1 #Healthcare Therapy 60.00 Medicine 15.00 DoctorVisit 100.00 GymMembership 30.00 Dentist 80.00 #Utilities CableTV 45.00 Electricity 50.00 Water 30.00 Gas 40.00 Internet 60.00 #Transportation BikeRepair 25.00 Gas 20.00 BusTicket 2.50 Taxi 15.00 TrainTicket 3.00 !tag3"),
															new("&2024-03-10 $Account1 @Contractor2 #Transportation BikeRepair 25.00 Gas 20.00 BusTicket 2.50 Taxi 15.00 TrainTicket 3.00 #Entertainment Book 15.00 Concert 50.00 Movie 12.00 Popcorn 5.00 Theater 30.00 #Groceries Milk 2.99 Bread 1.49 Eggs 3.99 Cheese 4.99 Meat 20.00 Fish 15.00 Fruits 10.00 Vegetables 8.00 Pasta 2.00 Sauce 1.50 !tag1"),
															new("&2024-03-06 $Account3 @Contractor2 #Groceries Meat 20.00 Fish 15.00 Fruits 10.00 Vegetables 8.00 Milk 2.99 Bread 1.49 Eggs 3.99 Cheese 4.99 Pasta 2.00 Sauce 1.50 #Healthcare Dentist 80.00 Medicine 15.00 DoctorVisit 100.00 GymMembership 30.00 Therapy 60.00 #Utilities CableTV 45.00 Electricity 50.00 Water 30.00 Gas 40.00 Internet 60.00 !tag3"),
															new("&2024-03-07 $Account1 @Contractor3 #Utilities Gas 40.00 Electricity 50.00 Water 30.00 Internet 60.00 CableTV 45.00 #Transportation TrainTicket 3.00 Gas 20.00 BusTicket 2.50 Taxi 15.00 BikeRepair 25.00 #Entertainment Concert 50.00 Movie 12.00 Popcorn 5.00 Theater 30.00 Book 15.00 !tag1"),
															new("&2024-03-08 $Account2 @Contractor4 #Entertainment Theater 30.00 Concert 50.00 Movie 12.00 Popcorn 5.00 Book 15.00 #Groceries Pasta 2.00 Sauce 1.50 Milk 2.99 Bread 1.49 Eggs 3.99 Cheese 4.99 Meat 20.00 Fish 15.00 Fruits 10.00 Vegetables 8.00 #Healthcare Therapy 60.00 Medicine 15.00 DoctorVisit 100.00 GymMembership 30.00 Dentist 80.00 !tag2"),
															new("&2024-03-02 $Account2 @Contractor2 #Transportation Gas 20.00 BusTicket 2.50 Taxi 15.00 TrainTicket 3.00 #Entertainment Movie 12.00 Popcorn 5.00 Concert 50.00 Theater 30.00 #Groceries Fruits 10.00 Vegetables 8.00 Meat 20.00 Fish 15.00 !tag2"),
															new("&2024-03-03 $Account3 @Contractor3 #Healthcare Medicine 15.00 DoctorVisit 100.00 GymMembership 30.00 Therapy 60.00 #Groceries Fruits 10.00 Vegetables 8.00 Meat 20.00 Fish 15.00 Pasta 2.00 Sauce 1.50 #Utilities Electricity 50.00 Water 30.00 Gas 40.00 Internet 60.00 !tag3"),
															new("&2024-03-04 $Account1 @Contractor4 #Utilities Internet 60.00 CableTV 45.00 #Healthcare GymMembership 30.00 Therapy 60.00 Dentist 80.00 #Transportation Gas 20.00 BusTicket 2.50 Taxi 15.00 TrainTicket 3.00 !tag1"),
															new("&2024-03-05 $Account2 @Contractor1 #Entertainment Concert 50.00 Movie 12.00 Popcorn 5.00 Theater 30.00 Book 15.00 #Transportation Taxi 15.00 Gas 20.00 BusTicket 2.50 TrainTicket 3.00 BikeRepair 25.00 #Groceries Pasta 2.00 Sauce 1.50 Milk 2.99 Bread 1.49 Eggs 3.99 Cheese 4.99 !tag2")
										};
										_snackbarService = snackbarService;
#endif
					}

					[RelayCommand]
					private void Edit(InscribedTransaction selectedTransaction)
					{
										if (_selectionService != null)
															_selectionService.SelectedTransaction = selectedTransaction;
					}

					[RelayCommand]
					private void Delete(InscribedTransaction selectedTransaction)
					{
										InscribedTransactions.Remove(selectedTransaction);
					}

					[RelayCommand]
					private void ProcessTransactions()
					{
										(IList<InscribedTransaction> successfullyProcessed, IList<InscribedTransaction> unsuccessfullyProcessed)? processingResult = _transactionsTextProcessor?.ProcessMultipleTransactions(InscribedTransactions);
										_snackbarService.Show("Transactions saved", $"Successfully processed {processingResult?.successfullyProcessed.Count ?? 0} out of {InscribedTransactions.Count}.", Wpf.Ui.Controls.ControlAppearance.Primary, null, TimeSpan.FromSeconds(20));
										InscribedTransactions = new ObservableCollection<InscribedTransaction>(processingResult?.unsuccessfullyProcessed ?? []);

					}
}
