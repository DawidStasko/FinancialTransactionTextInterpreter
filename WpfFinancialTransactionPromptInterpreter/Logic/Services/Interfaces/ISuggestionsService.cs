namespace WpfFinancialTransactionPromptInterpreter.Logic.Services.Interfaces;

public interface ISuggestionsService
{
    IEnumerable<string> GetSuggestions(string input);
}