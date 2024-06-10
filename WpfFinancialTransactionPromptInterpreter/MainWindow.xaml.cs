using Microsoft.Win32;
using System.Windows;
using Wpf.Ui;
using Wpf.Ui.Controls;
using WpfFinancialTransactionPromptInterpreter.ViewModels;

namespace WpfFinancialTransactionPromptInterpreter;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : FluentWindow
{

					public MainWindow(MainWindowVM mainWindowVM, ISnackbarService snackbarService)
					{
										InitializeComponent();
										this.DataContext = mainWindowVM;
										snackbarService.SetSnackbarPresenter(SnackbarPresenter);
					}

					private void CheckAllFiles()
					{
										//MainWindowVM mainWindowVM = (MainWindowVM)DataContext;
										//FileCheckResult result = mainWindowVM.CheckAllFiles();
										//if (FileCheckResult.HasErrors)
										//{

										//					MessageBox.Show("There are errors in the files. Please check the logs.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
										//}
										//else
										//{
										//					MessageBox.Show("All files are correct.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
										//}
					}

					private void OpenFileDialog(object sender, RoutedEventArgs e)
					{
										OpenFileDialog fileDialog = new();
										fileDialog.DefaultExt = ".xlsx";
										fileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
										bool? result = fileDialog.ShowDialog();
										if (result ?? false)
										{
															MainWindowVM mainWindowVM = (MainWindowVM)DataContext;
															mainWindowVM.SetNewFileName(fileDialog.FileName);
										}
					}

}