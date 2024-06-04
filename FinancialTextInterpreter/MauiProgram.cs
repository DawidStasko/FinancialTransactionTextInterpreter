using FinancialTextInterpreter.BusinessLogic;
using FinancialTextInterpreter.BusinessLogic.InfrastructureInterfaces;
using FinancialTextInterpreter.Infrastructure;
using Microsoft.Extensions.Logging;

namespace FinancialTextInterpreter;
public static class MauiProgram
{
					public static MauiApp CreateMauiApp()
					{
										MauiAppBuilder builder = MauiApp.CreateBuilder();
										builder
											.UseMauiApp<App>()
											.ConfigureFonts(fonts =>
											{
																fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
																fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
											});

										builder.Services.AddSingleton<ITransactionSaver, TransactionToXLSXSaver>(x => new TransactionToXLSXSaver("FinancialData.xlsx"));
										builder.Services.AddSingleton<ProceedTypedInTransactions>();
										builder.Services.AddSingleton<MainPage>();

#if DEBUG
										builder.Logging.AddDebug();
#endif

										return builder.Build();
					}
}
