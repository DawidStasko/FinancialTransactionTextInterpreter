using MAUITransactionsPromptInterpreter.AppLogic;
using MAUITransactionsPromptInterpreter.AppLogic.InfrastructureInterfaces;
using MAUITransactionsPromptInterpreter.Infrastructure;
using Microsoft.Extensions.Logging;

namespace MAUITransactionsPromptInterpreter;
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
										//builder.Services.AddSingleton<PredefinedData>(x => new PredefinedData("FinancialData.xlsx"));
										builder.Services.AddSingleton<MainPage>();
#if DEBUG
										builder.Logging.AddDebug();
#endif

										return builder.Build();
					}
}
