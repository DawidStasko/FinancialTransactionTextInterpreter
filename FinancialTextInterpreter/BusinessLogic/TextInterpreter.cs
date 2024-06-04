
using FinancialTextInterpreter.Domain;
using System.Globalization;

namespace FinancialTextInterpreter.BusinessLogic;
internal class TextInterpreter
{
					/// <summary>
					/// This text should have this format: 
					/// &dateOfTransaction $Account @contractor #category1 item1 price item2 price #category2 item3 price !tag item4 price 
					/// where ! @ # $ & are information about data stored in this string. All items after category belongs only to this category. 
					/// Tags should be assigned to first following item.
					/// </summary>
					/// <param name="text"></param>
					public static Transaction InterpretText(string text)
					{

										IList<string> words = [.. text.Split(' ')];
										Transaction transaction = new();
										string actualCategory = "";
										IList<string> tagsList = [];
										for (int i = 0; i < words.Count; i++)
										{
															string word = words[i];
															switch (word[0])
															{
																				case '&':
																									transaction.Date = DateTime.Parse(word.Substring(1));
																									break;
																				case '$':
																									transaction.Account = word.Substring(1);
																									break;
																				case '@':
																									transaction.Contractor = word.Substring(1);
																									break;
																				case '#':
																									actualCategory = word.Substring(1);
																									break;
																				case '!':
																									tagsList.Add(word.Substring(1));
																									break;
																				default:
																									Item item = new();
																									item.Name = word;
																									item.Category = actualCategory;
																									item.Tags = tagsList.ToArray();
																									string priceString = words[i + 1];
																									bool success = decimal.TryParse(priceString, CultureInfo.InvariantCulture, out decimal price);
																									if (success)
																									{
																														item.Price = price;
																														i++;
																									}
																									else
																									{
																														item.Price = null;
																									}
																									transaction.Items.Add(item);
																									break;
															}
										}
										return transaction;
					}
}