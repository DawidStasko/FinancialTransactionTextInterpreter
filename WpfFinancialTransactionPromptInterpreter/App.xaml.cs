using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Windows;

namespace WpfFinancialTransactionPromptInterpreter;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
					public IServiceProvider ServiceProvider { get; private set; }

					protected override void OnStartup(StartupEventArgs e)
					{

										base.OnStartup(e);

										AppDomain.CurrentDomain.UnhandledException += OnAppDomainUnhandledException;
										TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;

										ServiceCollection serviceCollection = new();
										ServicesConfigurator.ConfigureServices(serviceCollection);

										ServiceProvider = serviceCollection.BuildServiceProvider();
										MainWindow mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
										mainWindow.Show();

					}

					private void OnUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
					{
										MessageBox.Show("An unobserved exception occurred. Trace log will be saved to logs.", "Unobserved Exception");
										Log.Logger = new LoggerConfiguration()
															.MinimumLevel.Fatal()
															.WriteTo.File("FatalErrors/UnexpectedException-.txt", rollingInterval: RollingInterval.Minute)
															.CreateLogger();

										Log.Fatal(e.Exception, "An unobserved exception occurred.");
					}

					private void OnAppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
					{
										MessageBox.Show("An unhandled exception occurred. Trace log will be saved to logs.", "Unhandled Exception");
										Log.Logger = new LoggerConfiguration()
															.MinimumLevel.Fatal()
															.WriteTo.File("FatalErrors/UnhandledException-.txt", rollingInterval: RollingInterval.Minute)
															.CreateLogger();

										Log.Fatal(e.ExceptionObject as Exception, "An unhandled exception occurred.");
					}
}

