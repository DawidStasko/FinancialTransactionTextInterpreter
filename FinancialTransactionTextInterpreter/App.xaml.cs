using FinancialTransactionTextInterpreter.Model.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Globalization;
using System.Windows;

namespace FinancialTransactionTextInterpreter;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
					public IServiceProvider? ServiceProvider { get; private set; }

					protected override void OnStartup(StartupEventArgs e)
					{

										base.OnStartup(e);

										AppDomain.CurrentDomain.UnhandledException += OnAppDomainUnhandledException;
										TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;

										ServiceCollection serviceCollection = new();
										ServicesConfigurator.ConfigureServices(serviceCollection);

										ServiceProvider = serviceCollection.BuildServiceProvider();
										ArgumentNullException.ThrowIfNull(ServiceProvider);

										IConfig config = ServiceProvider.GetRequiredService<IConfig>();

										CultureInfo culture = new(config.ApplicationLanguage)
										{
															DateTimeFormat = new DateTimeFormatInfo()
															{
																				ShortDatePattern = "dd-MM-yyyy",
																				DateSeparator = "-",
																				LongTimePattern = "HH:mm:ss",
																				ShortTimePattern = "HH:mm"
															}

										};
										Thread.CurrentThread.CurrentCulture = culture;
										Thread.CurrentThread.CurrentUICulture = culture;


										MainWindow? mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
										ArgumentNullException.ThrowIfNull(mainWindow);
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
										MessageBox.Show($"An unhandled exception occurred. Trace log will be saved to logs.", "Unhandled Exception");
										Log.Logger = new LoggerConfiguration()
															.MinimumLevel.Fatal()
															.WriteTo.File("FatalErrors/UnhandledException-.txt", rollingInterval: RollingInterval.Minute)
															.CreateLogger();

										Log.Fatal(e.ExceptionObject as Exception, "An unhandled exception occurred.");
					}
}

