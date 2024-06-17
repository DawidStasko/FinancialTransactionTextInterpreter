using FinancialTransactionTextInterpreter.Logic.ExternalInterfaces;
using FinancialTransactionTextInterpreter.Logic.Services.Interfaces;
using FinancialTransactionTextInterpreter.Model.Interfaces;

namespace FinancialTransactionTextInterpreter.Logic.Services;
public class PredefinedDataService : IPredefinedDataService
{
					private readonly IConfig _config;

					public PredefinedDataService(ICategoriesRepository categoryRepository,
										IContractorsRepository contractorRepository,
										IAccountsRepository accountRepository,
										IConfig config)
					{
										Categories = categoryRepository.GetAllCategories().ToList();
										Contractors = contractorRepository.GetAllContractors().ToList();
										Accounts = accountRepository.GetAllAccounts().ToList();

										_config = config;
										_config.ConfigChanged += (sender, args) =>
										{
															Categories = categoryRepository.GetAllCategories().ToList();
															Contractors = contractorRepository.GetAllContractors().ToList();
															Accounts = accountRepository.GetAllAccounts().ToList();
										};
					}

					public IList<string> Categories { get; private set; }

					public IList<string> Contractors { get; private set; }

					public IList<string> Accounts { get; private set; }
}
