using System.Windows.Controls;

namespace WpfFinancialTransactionPromptInterpreter.Views;

/// <summary>
/// Interaction logic for PromptInput.xaml
/// </summary>
public partial class PromptInput : UserControl
{

					public PromptInput()
					{
										InitializeComponent();
					}

					private void txtInput_TextChanged(object sender, TextChangedEventArgs e)
					{
										//TextBox? textbox = sender as TextBox;
										//if (textbox != null)
										//					ActualWordAttachedProperty.SetActualWord(textbox, GetActualWord(textbox.Text, textbox.CaretIndex));
					}
}
