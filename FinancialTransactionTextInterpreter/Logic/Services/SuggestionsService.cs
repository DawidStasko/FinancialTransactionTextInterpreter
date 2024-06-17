using FinancialTransactionTextInterpreter.Logic.ExternalInterfaces;
using FinancialTransactionTextInterpreter.Logic.Services.Interfaces;

namespace FinancialTransactionTextInterpreter.Logic.Services;
public class SuggestionsService : ISuggestionsService
{
					private readonly IPredefinedDataService _predefinedDataService;
					private readonly ILastDateProvider _lastDateProvider;

					public SuggestionsService(IPredefinedDataService predefinedDataService, ILastDateProvider lastDateProvider)
					{
										_predefinedDataService = predefinedDataService;
										_lastDateProvider = lastDateProvider;
					}

					public IEnumerable<string> GetSuggestions(string input)
					{
										if (string.IsNullOrWhiteSpace(input))
															return Enumerable.Empty<string>();

										switch (input[0])
										{
															case '&':
																				return [_lastDateProvider.GetLastDate().ToString("dd-MM-yyyy")];
															case '#':
																				return _predefinedDataService.Categories.Where(c => c.ToLower().Contains(input.Substring(1).ToLower()));
															case '$':
																				return _predefinedDataService.Accounts.Where(a => a.ToLower().Contains(input.Substring(1).ToLower()));
															case '@':
																				return _predefinedDataService.Contractors.Where(c => c.ToLower().Contains(input.Substring(1).ToLower()));
															default:
																				return Enumerable.Empty<string>();
										}
					}

}
