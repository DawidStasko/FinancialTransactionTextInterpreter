using System.Windows.Controls;

namespace FinancialTransactionTextInterpreter.Views;
/// <summary>
/// Interaction logic for LanguageSelectorView.xaml
/// </summary>
public partial class LanguageSelectorView : UserControl
{

					public LanguageSelectorView()
					{
										InitializeComponent();
					}

					private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
					{
										if (e.RemovedItems.Count != e.AddedItems.Count)
										{
															return;
										}

										Wpf.Ui.Controls.MessageBox messageBox = new()
										{
															Title = Localization.Strings.LanguageSelector_MessageBoxTitle,
															Content = Localization.Strings.LanguageSelector_MessageBoxContent
										};
										_ = messageBox.ShowDialogAsync();

					}
}
