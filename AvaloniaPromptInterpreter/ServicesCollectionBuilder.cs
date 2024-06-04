using AvaloniaPromptInterpreter.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace AvaloniaPromptInterpreter;
public static class ServicesCollectionBuilder
{
					public static void RegisterAppServices(this IServiceCollection services)
					{
										services.AddSingleton<MainViewModel>();
					}
}
