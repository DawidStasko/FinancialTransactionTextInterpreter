using FinancialTransactionTextInterpreter.Logic.InfrastructureInterfaces;
using FinancialTransactionTextInterpreter.Logic.Services.Interfaces;

namespace FinancialTransactionTextInterpreter.Logic.Services;
public class SuggestionsService : ISuggestionsService
{
					private readonly IPredefinedDataService _predefinedDataService;
					private readonly ILastDateProvider _lastDateProvider;

					public SuggestionsService(IPredefinedDataService predefinedDataService, ILastDateProvider lastDateProvider)
					{
										ArgumentNullException.ThrowIfNull(predefinedDataService);
										ArgumentNullException.ThrowIfNull(lastDateProvider);

										_predefinedDataService = predefinedDataService;
										_lastDateProvider = lastDateProvider;
					}

					public IEnumerable<string> GetSuggestions(string input)
					{
										if (string.IsNullOrWhiteSpace(input))
															return [];

										switch (input[0])
										{
															case '&':
																				return [_lastDateProvider.GetLastDate().ToString("dd-MM-yyyy")];
															case '#':
																				return _predefinedDataService.Categories.Where(c => c.Contains(input.Substring(1), StringComparison.CurrentCultureIgnoreCase));
															case '$':
																				return _predefinedDataService.Accounts.Where(a => a.Contains(input.Substring(1), StringComparison.CurrentCultureIgnoreCase));
															case '@':
																				return _predefinedDataService.Contractors.Where(c => c.Contains(input.Substring(1), StringComparison.CurrentCultureIgnoreCase));
															default:
																				return [];
										}
					}

}
