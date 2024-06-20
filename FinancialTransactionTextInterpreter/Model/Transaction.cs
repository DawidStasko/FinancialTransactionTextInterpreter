using System.Text;

namespace FinancialTransactionTextInterpreter.Model;

public class Transaction
{
					public string? Account { get; set; }
					public DateOnly Date { get; set; }
					public string? Contractor { get; set; }
					public IList<Item> Items { get; set; } = [];
					public decimal TotalPrice => Items.Sum(x => x.Price ?? 0);

					public override string ToString()
					{
										StringBuilder builder = new();
										builder.Append("&" + Date.ToString("dd-MM-yyyy") + " ");
										builder.Append("$" + Account + " ");
										builder.Append("@" + Contractor + " ");
										string actualCategory = "NoCategory";
										foreach (Item item in Items)
										{
															if (item.Category != actualCategory)
																				actualCategory = item.Category ?? "NoCategory";
															builder.Append("#" + actualCategory + " ");
															builder.Append(item.Name + " ");
															builder.Append(item.Price + " ");
										}

										return builder.ToString();
					}
}
