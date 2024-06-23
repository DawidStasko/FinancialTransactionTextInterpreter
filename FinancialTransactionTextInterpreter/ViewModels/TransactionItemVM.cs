using CommunityToolkit.Mvvm.ComponentModel;
using FinancialTransactionTextInterpreter.Model;

namespace FinancialTransactionTextInterpreter.ViewModels;
internal partial class TransactionItemVM : ObservableObject
{

					[ObservableProperty]
					private InscribedTransaction? _inscribedTransaction;

					[ObservableProperty]
					[NotifyPropertyChangedFor(nameof(HasErrors))]
					private Result<IEnumerable<Transaction>>? _processingResult;

					public TransactionItemVM() { }

					public bool HasErrors => (!ProcessingResult?.IsSuccess) ?? true;

}
