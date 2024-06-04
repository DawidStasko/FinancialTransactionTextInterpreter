namespace WpfFinancialTransactionPromptInterpreter.Model.Interfaces;
public interface IConfig
{
					event EventHandler? ConfigChanged;

					string FinancialDataFullyQualifiedFileName { get; set; }
}
