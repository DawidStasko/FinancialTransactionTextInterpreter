namespace WpfFinancialTransactionPromptInterpreter.Logic.Services.Interfaces;
public interface IPredefinedDataService
{
					IList<string> Categories { get; }
					IList<string> Contractors { get; }
					IList<string> Accounts { get; }
}
