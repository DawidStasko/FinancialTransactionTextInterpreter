namespace MAUITransactionsPromptInterpreter.Views;

public partial class CustomEditor : ContentView
{
					public BindableProperty SuggestionsListProperty =
										BindableProperty.Create(nameof(SuggestionsList), typeof(IList<string>), typeof(CustomEditor),
															propertyChanged: (bindable, oldValue, newValue) =>
															{
																				CustomEditor control = (CustomEditor)bindable;
																				control.SuggestionsListView.ItemsSource = newValue as IList<string>;
															});

					public CustomEditor()
					{
										InitializeComponent();
					}

					public IList<string> SuggestionsList
					{
										get => GetValue(SuggestionsListProperty) as IList<string>;
										set => SetValue(SuggestionsListProperty, value);
					}

					private void Editor_TextChanged(object sender, TextChangedEventArgs e)
					{

					}
}