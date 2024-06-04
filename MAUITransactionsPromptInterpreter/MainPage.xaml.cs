using MAUITransactionsPromptInterpreter.AppLogic;
using MAUITransactionsPromptInterpreter.Infrastructure;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MAUITransactionsPromptInterpreter;

public partial class MainPage : ContentPage
{
					private string _inputText = string.Empty;
					private TypedInTransaction? _editedItem = null;
					private bool _showSuggestions = true;
					private readonly ProceedTypedInTransactions _transactionsProcessor;
					private readonly PredefinedData _predefinedData;

					public ObservableCollection<TypedInTransaction> TypedInTransactions { get; } = new ObservableCollection<TypedInTransaction>() {
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

					public ObservableCollection<string> SuggestionsList { get; set; } = new ObservableCollection<string>() { "aaaa", "bbbbb", "cccccc" };

					public string InputText
					{
										get => _inputText;
										set
										{
															_inputText = value;
															OnPropertyChanged();
										}
					}

					public bool ShowSuggestions
					{
										get => _showSuggestions;
										set
										{
															_showSuggestions = value;
															OnPropertyChanged();
										}
					}


					public MainPage(ProceedTypedInTransactions transactionsProcessor)
					{
										InitializeComponent();
										BindingContext = this;
										_transactionsProcessor = transactionsProcessor;
										_predefinedData = new PredefinedData("FinancialData.xlsx");
					}

					private void SubmitTransaction(object sender, EventArgs e)
					{
										if (_editedItem == null)
										{
															TypedInTransactions.Add(new(InputText));
															InputText = string.Empty;
															OnPropertyChanged(nameof(TypedInTransactions));
										}
										else
										{
															_editedItem.TransactionText = InputText;
															InputText = string.Empty;
															_editedItem = null;
															OnPropertyChanged(nameof(TypedInTransactions));
										}
					}

					private void ClearTransaction(object sender, EventArgs e)
					{
										InputText = string.Empty;
					}

					private void DeleteItem(object sender, EventArgs e)
					{
										MenuItem? item = sender as MenuItem;
										TypedInTransaction? transaction = item?.CommandParameter as TypedInTransaction;
										if (transaction == null)
															return;
										TypedInTransactions.Remove(transaction);
					}

					private void EditItem(object sender, EventArgs e)
					{
										MenuItem? item = sender as MenuItem;
										TypedInTransaction? transaction = item?.CommandParameter as TypedInTransaction;
										if (transaction == null)
															return;
										InputText = transaction.TransactionText;
										_editedItem = transaction;
					}

					private void ProcessTransactions(object sender, EventArgs e)
					{
										IEnumerable<string> transactions = TypedInTransactions.Select(t => t.TransactionText);
										_transactionsProcessor.Proceed(transactions);
					}

					private void InputTextChanged(object sender, TextChangedEventArgs e)
					{
										Editor? editor = sender as Editor;
										if (editor is null || string.IsNullOrEmpty(e.NewTextValue))
															return;

										string actualWord = GetActualWord(e.NewTextValue, editor.CursorPosition);

										switch (actualWord[0])
										{
															case '#':
																				string wordWithoutTag = actualWord.Substring(1);
																				IEnumerable<string> categories = _predefinedData.Categories.Where(c => c.Contains(wordWithoutTag));
																				SuggestionsList =
																									new ObservableCollection<string>(categories);
																				ShowSuggestions = true;
																				break;

															case '@':
																				string wordWithoutContractor = actualWord.Substring(1);
																				IEnumerable<string> contractors = _predefinedData.Contractors.Where(c => c.Contains(wordWithoutContractor));
																				SuggestionsList =
																									new ObservableCollection<string>(contractors);
																				ShowSuggestions = true;
																				break;

															case '$':
																				string wordWithoutAccount = actualWord.Substring(1);
																				IEnumerable<string> accounts = _predefinedData.Accounts.Where(c => c.Contains(wordWithoutAccount));
																				SuggestionsList =
																									new ObservableCollection<string>(accounts);
																				ShowSuggestions = true;
																				break;

															default:
																				SuggestionsList = new ObservableCollection<string>();
																				break;
										}

										if (SuggestionsList.Any())
															ShowSuggestions = true;
										else
															ShowSuggestions = false;


										OnPropertyChanged(nameof(SuggestionsList));
					}

					private string GetActualWord(String newTextValue, int cursorPosition)
					{
										if (cursorPosition == 0)
															return string.Empty;

										int currentCursorPosition = cursorPosition;
										char? processedLetter = null;
										LinkedList<char?> actualWord = new();
										StringBuilder actualStringBuilder = new();
										while (currentCursorPosition > 0)
										{
															processedLetter = newTextValue[currentCursorPosition - 1];
															if (processedLetter == ' ')
																				break;

															actualWord.AddFirst(processedLetter);
															actualStringBuilder.Insert(0, processedLetter);
															currentCursorPosition--;
										}


										currentCursorPosition = cursorPosition;
										while (currentCursorPosition < newTextValue.Length)
										{
															processedLetter = newTextValue[currentCursorPosition];
															if (processedLetter == ' ')
																				break;

															actualWord.AddLast(processedLetter);
															actualStringBuilder.Append(processedLetter);
															currentCursorPosition++;

										}

										return actualStringBuilder.ToString();
					}
}

public class TypedInTransaction : INotifyPropertyChanged
{
					private readonly Guid _guid = new();
					private string _transactionText = "";

					public event PropertyChangedEventHandler? PropertyChanged;

					public Guid Guid => _guid;
					public string TransactionText
					{
										get => _transactionText;
										set
										{
															_transactionText = value;
															OnPropertyChanged();
										}
					}
					public TypedInTransaction(string transactionText)
					{
										TransactionText = transactionText;
					}

					protected void OnPropertyChanged([CallerMemberName] string name = null)
					{
										PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
					}
}
