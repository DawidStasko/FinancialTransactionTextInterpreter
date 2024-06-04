namespace MAUITransactionsPromptInterpreter.Domain;
public class Transaction
{
					public string? Account { get; set; }
					public DateTime Date { get; set; }
					public string? Contractor { get; set; }
					public IList<Item> Items { get; } = [];
}
