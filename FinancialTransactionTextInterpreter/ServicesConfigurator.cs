using FinancialTransactionTextInterpreter.Infrastructure;
using FinancialTransactionTextInterpreter.Logic;
using FinancialTransactionTextInterpreter.Logic.ExternalInterfaces;
using FinancialTransactionTextInterpreter.Logic.Interfaces;
using FinancialTransactionTextInterpreter.Logic.Services;
using FinancialTransactionTextInterpreter.Logic.Services.Interfaces;
using FinancialTransactionTextInterpreter.Model.Interfaces;
using FinancialTransactionTextInterpreter.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Wpf.Ui;
using WpfFinancialTransactionPromptInterpreter;
using WpfFinancialTransactionPromptInterpreter.ViewModels;

namespace FinancialTransactionTextInterpreter;

internal class ServicesConfigurator
{
					internal static void ConfigureServices(ServiceCollection serviceCollection)
					{


										serviceCollection.AddLogging(x =>
										{
															Serilog.Core.Logger logger = new LoggerConfiguration()
																																	.MinimumLevel.Debug()
																																	.WriteTo.Logger(x => x.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information).WriteTo.File("Information/Information-.txt", rollingInterval: RollingInterval.Day))
																																	.WriteTo.Logger(x => x.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning).WriteTo.File("Warnings/Warnings-.txt", rollingInterval: RollingInterval.Day))
																																	.WriteTo.Logger(x => x.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error).WriteTo.File("Errors/Errors-.txt", rollingInterval: RollingInterval.Day))
																																	.WriteTo.Logger(x => x.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Fatal).WriteTo.File("FatalErrors/FatalErrors-.txt", rollingInterval: RollingInterval.Minute))
																																	.CreateLogger();
															x.AddSerilog(logger);
										});

										//Views
										serviceCollection.AddTransient<MainWindow>();


										//ViewModels
										serviceCollection.AddTransient<MainWindowVM>();
										serviceCollection.AddTransient<InscribedTransactionsListVM>();
										serviceCollection.AddTransient<PromptInputVM>();

										//Services
										serviceCollection.AddScoped<ITransactionInterpreterService, TransactionInterpreterService>();
										serviceCollection.AddScoped<ITransactionsSelectionService, TransactionsSelectionService>();
										serviceCollection.AddScoped<ITransactionsTextProcessor, TransactionsTextProcessor>();
										serviceCollection.AddScoped<IPredefinedDataService, PredefinedDataService>();
										serviceCollection.AddScoped<INewTransactionCreatedService, NewTransactionCreatedService>();
										serviceCollection.AddScoped<ISuggestionsService, SuggestionsService>();
										serviceCollection.AddSingleton<ISnackbarService, SnackbarService>();

										//Infrastructure
										serviceCollection.AddSingleton<IConfig, UserConfiguration>();
										serviceCollection.AddScoped<ICategoriesRepository, ExcelDataRepository>();
										serviceCollection.AddScoped<IAccountsRepository, ExcelDataRepository>();
										serviceCollection.AddScoped<IContractorsRepository, ExcelDataRepository>();
										serviceCollection.AddScoped<ITransactionsRepository, TransactionToXLSXSaver>();
										serviceCollection.AddScoped<ILastDateProvider, ExcelDataRepository>();
					}
}
