using CommunityToolkit.Mvvm.ComponentModel;
using FinancialTransactionTextInterpreter.Model.Interfaces;

namespace FinancialTransactionTextInterpreter.ViewModels;
public partial class LanguageSelectorVM : ObservableObject
{
					private readonly IConfig _config;

					[ObservableProperty]
					private Dictionary<string, string> _languages = new()
																								{
																												{ "English", "en-US" },
																												{ "Polski", "pl-PL" }
																								};

					[ObservableProperty]
					private string _selectedLanguage;

					public LanguageSelectorVM(IConfig config)
					{
										ArgumentNullException.ThrowIfNull(config);
										_config = config;
										_selectedLanguage = _languages.FirstOrDefault(x => x.Value == _config.ApplicationLanguage).Key;
					}

					partial void OnSelectedLanguageChanged(string value)
					{
										_config.ApplicationLanguage = Languages[value];
					}

}

