using FinancialTransactionTextInterpreter.Infrastructure;
using FinancialTransactionTextInterpreter.Localization;
using FinancialTransactionTextInterpreter.Model;
using FinancialTransactionTextInterpreter.Model.Interfaces;
using NSubstitute;

namespace FinancialTransactionTextInterpreterTests.InfrastructureTests;
public class TransactionToXLSXSaverTests
{
					IConfig _config;
					TransactionToXLSXSaver _transactionToXLSXSaver;

					public TransactionToXLSXSaverTests()
					{
										_config = Substitute.For<IConfig>();
										_transactionToXLSXSaver = new(_config);
					}

					[Theory]
					[InlineData(null)]
					[InlineData("")]
					[Trait("Category", "Unit")]
					public void SaveTransactions_WhenCalledWithEmptyFilePath_ReturnsResultWithErrorMessage(string? filePath)
					{
										_config.FinancialDataFullyQualifiedFileName.Returns(filePath);

										Result<Transaction> result = _transactionToXLSXSaver.Save(new Transaction());

										Assert.False(result.IsSuccess);
										Assert.NotNull(result.ErrorMessages);
										Assert.Single(result.ErrorMessages);
										Assert.Equal(Strings.ErrorMessage_FilePathIsNotSetInTheConfiguration, result.ErrorMessages.First());
					}

					[Theory]
					[InlineData("filePath")]
					[InlineData("filePath.xls")]
					[InlineData("filePath.cs")]
					[InlineData("filePath.xlsxx")]
					[Trait("Category", "Unit")]
					public void SaveTransactions_WhenCalledWithNonExcelFile_ReturnsResultWithErrorMessage(string? filePath)
					{
										_config.FinancialDataFullyQualifiedFileName.Returns(filePath);

										Result<Transaction> result = _transactionToXLSXSaver.Save(new Transaction());

										Assert.False(result.IsSuccess);
										Assert.NotNull(result.ErrorMessages);
										Assert.Single(result.ErrorMessages);
										Assert.Equal(Strings.ErrorMessage_FileIsNotAnExcelFile, result.ErrorMessages.First());
					}
}
