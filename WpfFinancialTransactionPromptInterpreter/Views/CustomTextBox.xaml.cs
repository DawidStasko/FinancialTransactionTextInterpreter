using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfFinancialTransactionPromptInterpreter.Views;
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
									DependencyProperty.Register("SelectedItem", typeof(object), typeof(CustomTextBox), new PropertyMetadata(null));

					public static readonly DependencyProperty SelectedDateProperty =
									DependencyProperty.Register("SelectedDate", typeof(object), typeof(CustomTextBox), new PropertyMetadata(null));

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

					public object SelectedItem
					{
										get { return GetValue(SelectedItemProperty); }
										set { SetValue(SelectedItemProperty, value); }
					}

					public object SelectedDate
					{
										get { return GetValue(SelectedDateProperty); }
										set { SetValue(SelectedDateProperty, value); }
					}

					public CustomTextBox()
					{
										InitializeComponent();
					}

					private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
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
										char? processedLetter = null;
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

										if (thisObject.SuggestionsList.Count > 0)
										{
															thisObject.popUp.IsOpen = true;
										}
										else
										{
															thisObject.popUp.IsOpen = false;
										}
					}

					private void popUp_Opened(object sender, EventArgs e)
					{
										int caretIndex = textBox.CaretIndex;
										while (caretIndex > 0 && textBox.Text[caretIndex - 1] != ' ')
										{
															caretIndex--;
										}
										Rect rect = textBox.GetRectFromCharacterIndex(caretIndex);
										popUp.PlacementRectangle = rect;
					}

					private void textBox_PreviewKeyDown(object sender, KeyEventArgs e)
					{
										if (SuggestionsList.Count == 0)
															return;

										if (e.Key == Key.Down)
										{
															if (SelectedItem == null)
															{
																				SelectedItem = SuggestionsList[0];
																				return;
															}
															else
															{
																				int actualSelectedItemIndex = SuggestionsList.IndexOf(SelectedItem.ToString());
																				SelectedItem = SuggestionsList[(actualSelectedItemIndex + 1) % SuggestionsList.Count];
															}
										}
										else if (e.Key == Key.Up)
										{
															if (SelectedItem == null)
															{
																				SelectedItem = SuggestionsList[SuggestionsList.Count - 1];
																				return;
															}
															else
															{
																				int actualSelectedItemIndex = SuggestionsList.IndexOf(SelectedItem.ToString());
																				SelectedItem = SuggestionsList[(actualSelectedItemIndex - 1 + SuggestionsList.Count) % SuggestionsList.Count];
															}
										}
										else if (e.Key == Key.Enter || e.Key == Key.Space || e.Key == Key.Tab)
										{

															if (SelectedItem != null)
															{
																				int caretIndex = textBox.CaretIndex;
																				int wordToReplaceLength = ActualWord.Length;

																				string textToInsert = ActualWord[0] + SelectedItem.ToString() + " ";
																				string textBeforeActualWord = TextValue.Substring(0, caretIndex - wordToReplaceLength);
																				string textAfterActualWord = TextValue.Substring(caretIndex);

																				TextValue = textBeforeActualWord + textToInsert + textAfterActualWord;
																				textBox.CaretIndex = caretIndex + textToInsert.Length - wordToReplaceLength + 1;
																				e.Handled = true;

															}
										}
					}
}
