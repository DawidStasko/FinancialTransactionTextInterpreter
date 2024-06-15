namespace WpfFinancialTransactionPromptInterpreter.Model;

public class Transaction
{
					public string? Account { get; set; }
					public DateOnly Date { get; set; }
					public string? Contractor { get; set; }
					public IList<Item> Items { get; set; } = [];
}
