using ClosedXML.Excel;

namespace MAUITransactionsPromptInterpreter.Infrastructure;

public class PredefinedData
{
					public IList<string> Categories { get; set; } = new List<string>();
					public IList<string> Accounts { get; set; } = new List<string>();
					public IList<string> Contractors { get; set; } = new List<string>();

					public PredefinedData(string fileName, string filePath = "")
					{
										using XLWorkbook workbook = new(Path.Combine(filePath, fileName));
										{
															IXLWorksheet worksheet = workbook.Worksheet("PredefinedData");
															IXLTable categoriesTable = worksheet.Tables.First(x => x.Name == "Categories");
															foreach (IXLTableRow? category in categoriesTable.DataRange.Rows())
															{
																				Categories.Add(category.FirstCell().GetString());
															}

															IXLTable accountsTable = worksheet.Tables.First(x => x.Name == "Accounts");
															foreach (IXLTableRow? account in accountsTable.DataRange.Rows())
															{
																				Accounts.Add(account.FirstCell().GetString());
															}

															IXLTable contractorsTable = worksheet.Tables.First(x => x.Name == "Contractors");
															foreach (IXLTableRow? contractor in contractorsTable.DataRange.Rows())
															{
																				Contractors.Add(contractor.FirstCell().GetString());
															}
										}
					}
}
