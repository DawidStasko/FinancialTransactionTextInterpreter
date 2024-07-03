using FinancialTransactionTextInterpreter.ViewModels;
using System.Windows;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace FinancialTransactionTextInterpreter;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : FluentWindow
{
					public static readonly DependencyProperty FilePathProperty =
									DependencyProperty.Register("FilePath", typeof(string), typeof(MainWindow));

					public string FilePath
					{
										get { return (string)GetValue(FilePathProperty); }
										set { SetValue(FilePathProperty, value); }
					}

					public MainWindow(MainWindowVM mainWindowVM, ISnackbarService snackbarService)
					{
										InitializeComponent();
										this.DataContext = mainWindowVM;
										snackbarService.SetSnackbarPresenter(SnackbarPresenter);
					}

					private void OpenExcelFile(object sender, RoutedEventArgs e)
					{

					}

}
