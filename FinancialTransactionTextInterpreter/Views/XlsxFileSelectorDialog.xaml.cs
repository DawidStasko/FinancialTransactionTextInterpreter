using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

namespace FinancialTransactionTextInterpreter.Views;

/// <summary>
/// Interaction logic for XlsxFileSelectorDialog.xaml
/// </summary>
public partial class XlsxFileSelectorDialog : UserControl
{
					public string FilePath
					{
										get { return (string)GetValue(FilePathProperty); }
										set { SetValue(FilePathProperty, value); }
					}

					public static readonly DependencyProperty FilePathProperty =
									DependencyProperty.Register("FilePath", typeof(string), typeof(XlsxFileSelectorDialog), new PropertyMetadata(null));


					public XlsxFileSelectorDialog()
					{
										InitializeComponent();
					}

					private void OnClick(object sender, RoutedEventArgs e)
					{
										OpenFileDialog fileDialog = new();
										fileDialog.DefaultExt = ".xlsx";
										fileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
										bool? result = fileDialog.ShowDialog();
										if (result ?? false)
										{
															FilePath = fileDialog.FileName;
										}
					}
}
