namespace WpfFinancialTransactionPromptInterpreter.Logic.ExternalInterfaces;
public interface ICategoriesRepository
{
					IEnumerable<string> GetAllCategories();

}
