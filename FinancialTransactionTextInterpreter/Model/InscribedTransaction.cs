using CommunityToolkit.Mvvm.ComponentModel;

namespace FinancialTransactionTextInterpreter.Model;

public class InscribedTransaction : ObservableObject
{
					private Guid _id = Guid.NewGuid();
					public Guid Id => _id;
					private string? _text;
					public string? Text
					{
										get => _text;
										set
										{
															_text = value;
															OnPropertyChanged();
										}
					}

					public InscribedTransaction(string text)
					{
										Text = text;
					}
}
