namespace FinancialTransactionTextInterpreter.Logic.ExternalInterfaces;
public interface ICategoriesRepository
{
					IEnumerable<string> GetAllCategories();

}
