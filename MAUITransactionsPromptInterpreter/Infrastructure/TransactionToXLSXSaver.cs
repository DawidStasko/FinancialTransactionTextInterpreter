using ClosedXML.Excel;
using MAUITransactionsPromptInterpreter.AppLogic.InfrastructureInterfaces;
using MAUITransactionsPromptInterpreter.Domain;

namespace MAUITransactionsPromptInterpreter.Infrastructure;

public class TransactionToXLSXSaver : ITransactionSaver
{
					private string? _fileName;
					private string? _filePath;

					public TransactionToXLSXSaver(string fileName, string? filePath = null)
					{
										_fileName = fileName;
										_filePath = filePath;
					}

					public void Save(Transaction transaction)
					{
										string worksheetName = GetWorksheetName(transaction.Date);
										using XLWorkbook workbook = new(_fileName);
										IXLWorksheets worksheetsList = workbook.Worksheets;
										IXLWorksheet worksheet = workbook.Worksheets.FirstOrDefault(w => w.Name == worksheetName) ?? workbook.Worksheets.Add(worksheetName);
										IXLRow row = worksheet.LastRowUsed()?.RowBelow() ?? worksheet.FirstRow();
										foreach (Item item in transaction.Items)
										{
															row.Cell(1).Value = transaction.Date;
															row.Cell(2).Value = item.Name;
															row.Cell(3).Value = item.Price;
															row.Cell(4).Value = item.Category;
															row.Cell(5).Value = transaction.Account;
															row.Cell(6).Value = transaction.Contractor;
															row.Cell(7).Value = string.Join(", ", item.Tags);
															row = row.RowBelow();
										}
										workbook.Save();
					}

					private static string GetWorksheetName(DateTime date)
					{
										int month = date.Month;
										int year = date.Year;

										return $"{month}-{year}";
					}
}
