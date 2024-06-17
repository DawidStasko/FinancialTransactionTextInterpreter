using CommunityToolkit.Mvvm.ComponentModel;

namespace FinancialTransactionTextInterpreter.ViewModels;
public partial class HelpBoxVM : ObservableObject
{
					[ObservableProperty]
					private string _helpTitle = "Help";

					[ObservableProperty]
					private string _generalHelpContent = "Help Contetn";

					[ObservableProperty]
					private string _tagsSectionTitile = "Tags";

					[ObservableProperty]
					private string _tagsSectionContent = "Tags Content";

					[ObservableProperty]
					private string _examplesSectionTitle = "";

					[ObservableProperty]
					private string _exampleSectionContent = "";

}
