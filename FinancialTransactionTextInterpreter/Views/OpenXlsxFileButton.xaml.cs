using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace FinancialTransactionTextInterpreter.Views;
public partial class OpenXlsxFileButton : UserControl
{
					public string FilePath
					{
										get { return (string)GetValue(FilePathProperty); }
										set { SetValue(FilePathProperty, value); }
					}

					public static readonly DependencyProperty FilePathProperty =
									DependencyProperty.Register("FilePath", typeof(string), typeof(OpenXlsxFileButton), new PropertyMetadata(null));

					public OpenXlsxFileButton()
					{
										InitializeComponent();
					}

					private void OnClick(object sender, RoutedEventArgs e)
					{
										if (string.IsNullOrEmpty(FilePath))
										{
															return;
										}

										Process excelFile = new()
										{
															StartInfo = new ProcessStartInfo()
															{
																				FileName = "powershell.exe",
																				Arguments = $"-Command \"& {{Start-Process {FilePath} }}\"",
																				UseShellExecute = true
															}
										};

										excelFile.Start();
					}
}
