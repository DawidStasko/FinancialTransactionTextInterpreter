using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FinancialTransactionTextInterpreter.Views;
/// <summary>
/// Interaction logic for CustomTextBox.xaml
/// </summary>
public partial class CustomTextBox : UserControl
{
					public static readonly DependencyProperty TextValueProperty =
									DependencyProperty.Register("TextValue", typeof(string), typeof(CustomTextBox), new PropertyMetadata(string.Empty));

					public static readonly DependencyProperty ActualWordProperty =
									DependencyProperty.Register("ActualWord", typeof(string), typeof(CustomTextBox), new PropertyMetadata(string.Empty));

					public static readonly DependencyProperty SuggestionsListProperty =
									DependencyProperty.Register("SuggestionsList", typeof(IList<string>), typeof(CustomTextBox), new PropertyMetadata(null, OnSuggestionsListPropertyChanged));

					public static readonly DependencyProperty SelectedItemProperty =
									DependencyProperty.Register("SelectedItem", typeof(string), typeof(CustomTextBox), new PropertyMetadata(null));

					public static readonly DependencyProperty SelectedDateProperty =
									DependencyProperty.Register("SelectedDate", typeof(DateTime?), typeof(CustomTextBox), new PropertyMetadata(null));

					public string TextValue
					{
										get { return (string)GetValue(TextValueProperty); }
										set { SetValue(TextValueProperty, value); }
					}

					public string ActualWord
					{
										get { return (string)GetValue(ActualWordProperty); }
										set { SetValue(ActualWordProperty, value); }
					}

					public IList<string> SuggestionsList
					{
										get { return (IList<string>)GetValue(SuggestionsListProperty); }
										set { SetValue(SuggestionsListProperty, value); }
					}

					public string? SelectedItem
					{
										get { return (string?)GetValue(SelectedItemProperty); }
										set { SetValue(SelectedItemProperty, value); }
					}

					public DateTime? SelectedDate
					{
										get { return (DateTime?)GetValue(SelectedDateProperty); }
										set { SetValue(SelectedDateProperty, value); }
					}

					public CustomTextBox()
					{
										InitializeComponent();
					}

					private void TextInput_TextChanged(object sender, TextChangedEventArgs e)
					{
										TextBox? textBox = sender as TextBox;
										if (textBox != null)
															ActualWord = GetActualWord(textBox.Text, textBox.CaretIndex);
					}

					private string GetActualWord(string textInput, int caretIndex)
					{
										if (caretIndex == 0)
															return string.Empty;

										int currentCaretIndex = caretIndex;
										char? processedLetter;
										LinkedList<char?> actualWord = new();
										StringBuilder actualStringBuilder = new();
										while (currentCaretIndex > 0)
										{
															processedLetter = textInput[currentCaretIndex - 1];
															if (processedLetter == ' ')
																				break;

															actualWord.AddFirst(processedLetter);
															actualStringBuilder.Insert(0, processedLetter);
															currentCaretIndex--;
										}

										currentCaretIndex = caretIndex;
										while (currentCaretIndex < textInput.Length)
										{
															processedLetter = textInput[currentCaretIndex];
															if (processedLetter == ' ')
																				break;

															actualWord.AddLast(processedLetter);
															actualStringBuilder.Append(processedLetter);
															currentCaretIndex++;
										}

										return actualStringBuilder.ToString();
					}

					public static void OnSuggestionsListPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
					{
										if (dependencyObject is not CustomTextBox thisObject)
										{
															return;
										}

										thisObject.SelectedItem = null;

										if (thisObject.SuggestionsList.Count == 0)
										{
															thisObject.SuggestionsPopup.IsOpen = false;
															return;
										}
										bool isDate = DateTime.TryParse(thisObject.SuggestionsList.FirstOrDefault(), out DateTime result);
										if (isDate)
										{

															thisObject.SelectedDate = result;
															SetCalendarVisible(thisObject);
															thisObject.SuggestionsPopup.IsOpen = true;
															return;
										}

										SetSuggestionsListVisible(thisObject);
										thisObject.SuggestionsPopup.IsOpen = true;
					}

					private static void SetSuggestionsListVisible(CustomTextBox thisObject)
					{
										thisObject.SuggestionsListBox.Visibility = Visibility.Visible;
										thisObject.DatePicker.Visibility = Visibility.Collapsed;
					}
					private static void SetCalendarVisible(CustomTextBox thisObject)
					{
										thisObject.SuggestionsListBox.Visibility = Visibility.Collapsed;
										thisObject.DatePicker.Visibility = Visibility.Visible;
					}

					private void popUp_Opened(object sender, EventArgs e)
					{
										int caretIndex = TextInput.CaretIndex;
										while (caretIndex > 0 && TextInput.Text[caretIndex - 1] != ' ')
										{
															caretIndex--;
										}
										Rect rect = TextInput.GetRectFromCharacterIndex(caretIndex);
										SuggestionsPopup.PlacementRectangle = rect;
										SuggestionsPopup.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
					}

					private void TextInput_PreviewKeyDown(object sender, KeyEventArgs e)
					{
										if (SuggestionsList.Count == 0) return;

										Action<KeyEventArgs> action = e.Key switch
										{

															Key.Down => MoveDown,
															Key.Up => MoveUp,
															Key.Right => MoveRight,
															Key.Left => MoveLeft,
															Key.Enter or Key.Tab or Key.Space => ConfirmSelection,
															Key.Escape => CancelSelection,
															Key.Back => DeleteLastLetter,
															_ => (e) => { } // No operation for unhandled keys
										};
										action(e);
					}

					private void DeleteLastLetter(RoutedEventArgs e)
					{
										int caretIndex = TextInput.CaretIndex;
										if (caretIndex > 0)
										{
															TextValue = TextValue.Remove(caretIndex - 1, 1);
															TextInput.CaretIndex = caretIndex - 1;
										}
										e.Handled = true;
					}

					private void CancelSelection(RoutedEventArgs e)
					{
										e.Handled = true;
										SelectedDate = null;
										SelectedItem = null;
										SuggestionsPopup.IsOpen = false;
					}

					private void ConfirmSelection(RoutedEventArgs e)
					{
										e.Handled = true;

										(string newText, int newIndex)? result = null;
										if (SelectedDate != null)
										{
															result = ReplaceActualWordWithSuggestionAndCalculateNewIndex(SelectedDate!.Value.ToString("dd-MM-yyyy"));
										}

										if (SelectedItem != null && result == null)
										{
															result = ReplaceActualWordWithSuggestionAndCalculateNewIndex(SelectedItem!);
										}

										if (SelectedItem == null && SuggestionsList != null && SuggestionsList.Count > 0)
										{
															result = ReplaceActualWordWithSuggestionAndCalculateNewIndex(SuggestionsList[0]);
										}

										if (result.HasValue)
										{
															TextValue = result.Value.newText;
															TextInput.CaretIndex = result.Value.newIndex;
										}

										SelectedDate = null;
										SelectedItem = null;
										return;
					}

					private (string newText, int newIndex) ReplaceActualWordWithSuggestionAndCalculateNewIndex(string textToInsert)
					{
										int caretIndex = TextInput.CaretIndex;
										int wordToReplaceLength = ActualWord.Length;
										string textBeforeActualWord = TextValue.Substring(0, caretIndex - wordToReplaceLength);
										string textAfterActualWord = TextValue.Substring(caretIndex);

										string newText = $"{textBeforeActualWord}{ActualWord[0]}{textToInsert} {textAfterActualWord}";
										int newIndex = caretIndex + textToInsert.Length - wordToReplaceLength + 3;
										return (newText, newIndex);

					}

					private void MoveUp(RoutedEventArgs e)
					{
										e.Handled = true;

										if (this.DatePicker.Visibility == Visibility.Visible)
										{
															SelectedDate = SelectedDate == null ? DateTime.Now : ((DateTime)SelectedDate).AddDays(-7);
															return;
										}

										if (SelectedItem == null)
										{
															SelectedItem = SuggestionsList[SuggestionsList.Count - 1];
										}
										else
										{
															int actualSelectedItemIndex = SuggestionsList.IndexOf(SelectedItem.ToString());
															SelectedItem = SuggestionsList[(actualSelectedItemIndex - 1 + SuggestionsList.Count) % SuggestionsList.Count];
										}

					}

					private void MoveDown(RoutedEventArgs e)
					{
										e.Handled = true;
										if (this.DatePicker.Visibility == Visibility.Visible)
										{
															SelectedDate = SelectedDate == null ? DateTime.Now : ((DateTime)SelectedDate).AddDays(7);
															return;
										}

										if (SelectedItem == null)
										{
															SelectedItem = SuggestionsList[0];
										}
										else
										{
															int actualSelectedItemIndex = SuggestionsList.IndexOf(SelectedItem.ToString());
															SelectedItem = SuggestionsList[(actualSelectedItemIndex + 1) % SuggestionsList.Count];
										}
					}

					private void MoveRight(RoutedEventArgs e)
					{
										e.Handled = true;
										if (this.DatePicker.Visibility == Visibility.Visible)
										{
															SelectedDate = SelectedDate == null ? DateTime.Now : ((DateTime)SelectedDate).AddDays(1);
															return;
										}
					}

					private void MoveLeft(RoutedEventArgs e)
					{
										e.Handled = true;
										if (this.DatePicker.Visibility == Visibility.Visible)
										{
															SelectedDate = SelectedDate == null ? DateTime.Now : ((DateTime)SelectedDate).AddDays(-1);
															return;
										}
					}

					private void SuggestionsListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
					{
										ListViewItem? item = ItemsControl.ContainerFromElement(sender as ListView, e.OriginalSource as DependencyObject) as ListViewItem;
										if (item != null)
										{
															ConfirmSelection(e);
										}
					}
}
