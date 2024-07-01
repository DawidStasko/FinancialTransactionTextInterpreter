using CommunityToolkit.Mvvm.ComponentModel;

namespace FinancialTransactionTextInterpreter.Model;

public partial class InscribedTransaction : ObservableObject
{
					public Guid Id { get; private set; }

					[ObservableProperty]
					[NotifyPropertyChangedFor(nameof(HasErrors))]
					private Result<IList<Transaction>> _processingResult;

					[ObservableProperty]
					private string _text = "";

					public bool HasErrors => (!ProcessingResult?.IsSuccess) ?? true;

					public InscribedTransaction(string text)
					{
										Id = Guid.NewGuid();
										ProcessingResult = new Result<IList<Transaction>>() { ErrorMessages = new List<string>() { Localization.Strings.ErrorMessage_ProcessingWasNotDoneYet } };
										Text = text;
					}
}
