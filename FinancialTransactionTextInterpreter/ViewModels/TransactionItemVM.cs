using CommunityToolkit.Mvvm.ComponentModel;
using FinancialTransactionTextInterpreter.Model;
using System.ComponentModel;

namespace FinancialTransactionTextInterpreter.ViewModels;
internal partial class TransactionItemVM : ObservableObject
{

					[ObservableProperty]
					private InscribedTransaction _inscribedTransaction;

					[ObservableProperty]
					private Result<IEnumerable<Transaction>> _processingResult;

					public TransactionItemVM()
					{
										this.PropertyChanged += TransactionItemVM_PropertyChanged;
					}

					private void TransactionItemVM_PropertyChanged(object? sender, PropertyChangedEventArgs e)
					{
										if (e.PropertyName == nameof(ProcessingResult))
										{
															OnPropertyChanged(nameof(HasErrors));
										}
					}

					public bool HasErrors => (!ProcessingResult?.IsSuccess) ?? true;


}
