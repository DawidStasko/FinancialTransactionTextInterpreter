using FinancialTransactionTextInterpreter.Infrastructure;
using FinancialTransactionTextInterpreter.Model.Interfaces;
using NSubstitute;
namespace FinancialTransactionTextInterpreterTests.InfrastructureTests;
public class ExcelDataRepositoryTests
{
					IConfig _config;
					ExcelDataRepository _excelDataRepository;

					public ExcelDataRepositoryTests()
					{
										_config = Substitute.For<IConfig>();
										_excelDataRepository = new(_config);
					}

					[Theory]
					[InlineData(null)]
					[InlineData("")]
					[Trait("Category", "Unit")]
					public void GetAllAccounts_WhenCalledWithEmptyFilePath_ReturnsEmptyArray(string? filePath)
					{
										_config.FinancialDataFullyQualifiedFileName.Returns(filePath);

										IEnumerable<string> result = _excelDataRepository.GetAllAccounts();

										Assert.Empty(result);
					}

					[Theory]
					[InlineData(null)]
					[InlineData("")]
					[Trait("Category", "Unit")]
					public void GetAllCategories_WhenCalledWithEmptyFilePath_ReturnsEmptyArray(string? filePath)
					{
										_config.FinancialDataFullyQualifiedFileName.Returns(filePath);

										IEnumerable<string> result = _excelDataRepository.GetAllCategories();

										Assert.Empty(result);
					}

					[Theory]
					[InlineData(null)]
					[InlineData("")]
					[Trait("Category", "Unit")]
					public void GetAllContractors_WhenCalledWithEmptyFilePath_ReturnsEmptyArray(string? filePath)
					{
										_config.FinancialDataFullyQualifiedFileName.Returns(filePath);

										IEnumerable<string> result = _excelDataRepository.GetAllContractors();

										Assert.Empty(result);
					}

					[Theory]
					[InlineData(null)]
					[InlineData("")]
					[Trait("Category", "Unit")]
					public void GetLastDate_WhenCalledWithEmptyFilePath_ReturnsTodayDate(string? filePath)
					{
										_config.FinancialDataFullyQualifiedFileName.Returns(filePath);

										DateOnly result = _excelDataRepository.GetLastDate();

										Assert.Equal(DateOnly.FromDateTime(DateTime.Today), result);
					}

}
