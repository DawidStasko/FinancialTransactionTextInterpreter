namespace WpfFinancialTransactionPromptInterpreter.Model;

public class Item
{
					public string? Name { get; set; }
					public decimal? Price { get; set; }
					public string? Category { get; set; }
					public string[] Tags { get; set; } = [];
}
