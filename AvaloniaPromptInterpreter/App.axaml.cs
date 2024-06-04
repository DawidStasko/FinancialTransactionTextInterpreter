using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using AvaloniaPromptInterpreter.ViewModels;
using AvaloniaPromptInterpreter.Views;
using Microsoft.Extensions.DependencyInjection;

namespace AvaloniaPromptInterpreter;

public partial class App : Application
{
					public override void Initialize()
					{
										AvaloniaXamlLoader.Load(this);
					}

					public override void OnFrameworkInitializationCompleted()
					{
										// Line below is needed to remove Avalonia data validation.
										// Without this line you will get duplicate validations from both Avalonia and CT
										BindingPlugins.DataValidators.RemoveAt(0);

										// Register all the services needed for the application to run
										ServiceCollection collection = new();
										collection.RegisterAppServices();

										// Creates a ServiceProvider containing services from the provided IServiceCollection
										ServiceProvider services = collection.BuildServiceProvider();

										MainViewModel vm = services.GetRequiredService<MainViewModel>();

										if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
										{
															desktop.MainWindow = new MainWindow
															{
																				DataContext = vm
															};
										}
										else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
										{
															singleViewPlatform.MainView = new MainView
															{
																				DataContext = vm
															};
										}

										base.OnFrameworkInitializationCompleted();
					}
}
