namespace FinancialTransactionTextInterpreter.Logic.InfrastructureInterfaces;
public interface ICategoriesRepository
{
					IEnumerable<string> GetAllCategories();

}
