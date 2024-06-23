using ClosedXML.Excel;
using FinancialTransactionTextInterpreter.Logic.InfrastructureInterfaces;
using FinancialTransactionTextInterpreter.Model.Interfaces;
using System.Globalization;

namespace FinancialTransactionTextInterpreter.Infrastructure;
public class ExcelDataRepository : ICategoriesRepository, IAccountsRepository, IContractorsRepository, ILastDateProvider
{
					private readonly IConfig _config;

					public ExcelDataRepository(IConfig config)
					{
										ArgumentNullException.ThrowIfNull(config);

										_config = config;
					}

					public IEnumerable<string> GetAllAccounts()
					{
										if (string.IsNullOrEmpty(_config.FinancialDataFullyQualifiedFileName))
															return [];

										List<string> accounts = new();
										using XLWorkbook workbook = new(_config.FinancialDataFullyQualifiedFileName);
										{
															IXLWorksheet worksheet = workbook.Worksheet("PredefinedData");
															IXLTable accountsTable = worksheet.Tables.First(x => x.Name == "Accounts");
															foreach (IXLTableRow? account in accountsTable.DataRange.Rows())
															{
																				accounts.Add(account.FirstCell().GetString());
															}
										}

										return accounts;
					}

					public IEnumerable<string> GetAllCategories()
					{
										if (string.IsNullOrEmpty(_config.FinancialDataFullyQualifiedFileName))
															return [];

										List<string> categories = new();
										using XLWorkbook workbook = new(_config.FinancialDataFullyQualifiedFileName);
										{
															IXLWorksheet worksheet = workbook.Worksheet("PredefinedData");
															IXLTable categoriesTable = worksheet.Tables.First(x => x.Name == "Categories");
															foreach (IXLTableRow? category in categoriesTable.DataRange.Rows())
															{
																				categories.Add(category.FirstCell().GetString());
															}
										}

										return categories;
					}

					public IEnumerable<string> GetAllContractors()
					{
										if (string.IsNullOrEmpty(_config.FinancialDataFullyQualifiedFileName))
															return [];

										List<string> contractors = new();
										using XLWorkbook workbook = new(_config.FinancialDataFullyQualifiedFileName);
										{
															IXLWorksheet worksheet = workbook.Worksheet("PredefinedData");
															IXLTable contractorsTable = worksheet.Tables.First(x => x.Name == "Contractors");
															foreach (IXLTableRow? contractor in contractorsTable.DataRange.Rows())
															{
																				contractors.Add(contractor.FirstCell().GetString());
															}
										}

										return contractors;
					}

					public DateOnly GetLastDate()
					{
										if (string.IsNullOrEmpty(_config.FinancialDataFullyQualifiedFileName))
															return DateOnly.FromDateTime(DateTime.Now);

										using XLWorkbook workbook = new(_config.FinancialDataFullyQualifiedFileName);
										IXLWorksheet? worksheet = GetLastMothWorksheet(workbook);
										IXLRow? lastRow = worksheet?.LastRowUsed();

										if (lastRow == null)
															return DateOnly.FromDateTime(DateTime.Now);

										DateTime lastDate = lastRow.Cell(1).GetDateTime();
										return DateOnly.FromDateTime(lastDate);

					}

					private IXLWorksheet? GetLastMothWorksheet(XLWorkbook workbook)
					{
										IXLWorksheets allWorksheet = workbook.Worksheets;
										IList<DateOnly> worksheetNamesAsDates = [];
										foreach (IXLWorksheet worksheet in allWorksheet)
										{
															if (DateTime.TryParseExact(worksheet.Name, "MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
																				worksheetNamesAsDates.Add(DateOnly.FromDateTime(date));
										}

										if (worksheetNamesAsDates.Count == 0)
															return null;

										DateOnly lastDate = worksheetNamesAsDates.Max();
										return allWorksheet.First(x => x.Name == lastDate.ToString("MM-yyyy"));
					}
}
