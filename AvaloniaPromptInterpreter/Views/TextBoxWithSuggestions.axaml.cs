using Avalonia.Controls;
using System.Collections.Generic;

namespace AvaloniaPromptInterpreter.Views;
public partial class TextBoxWithSuggestions : UserControl
{
					public TextBoxWithSuggestions()
					{
										InitializeComponent();
										DataContext = this;
					}

					private void TextInputChanged(object? sender, Avalonia.Controls.TextChangedEventArgs e)
					{
										if (!string.IsNullOrEmpty(TextInput.Text))
										{
															// Populate the ListBox based on the TextBox's input.

															SuggestionsList.ItemsSource = new List<string>(){
																				"aaaa",
																				"bbbb",
																				"cccc" };

															// Show the Popup if there are any suggestions.
															SuggestionsPopup.IsOpen = !SuggestionsPopup.IsOpen;

															//// Position the Popup.
															//// Get the position of the caret.
															//Rect caretRect = TextInput.GetRectFromCharacterIndex(TextInput.CaretIndex);
															//// Convert the position from the TextBox's coordinate space to the window's coordinate space.
															//Point caretPoint = TextInput.PointToScreen(new Point(caretRect.X, caretRect.Y));
															//// Position the Popup.
															//SuggestionsPopup.HorizontalOffset = caretPoint.X;
															//SuggestionsPopup.VerticalOffset = caretPoint.Y + caretRect.Height;
										}
					}//
}