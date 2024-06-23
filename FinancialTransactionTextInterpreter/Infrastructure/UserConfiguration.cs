using FinancialTransactionTextInterpreter.Model.Exceptions;
using FinancialTransactionTextInterpreter.Model.Interfaces;
using Newtonsoft.Json.Linq;
using System.IO;

namespace FinancialTransactionTextInterpreter.Infrastructure;
public class UserConfiguration : IConfig
{
					private string _fileName = "";
					private string _financialDataFullyQualifiedFileName = "";

					public UserConfiguration()
					{
										_fileName = "UserSettings.json";
										if (!File.Exists(_fileName))
										{
															File.Create(_fileName).Close();
															File.WriteAllText(_fileName, $"{{ \"{nameof(FinancialDataFullyQualifiedFileName)}\": \"\" }}");
										}

										InitializeConfiguration();

					}

					private void InitializeConfiguration()
					{
										JObject settings = JObject.Parse(File.ReadAllText(_fileName));
										_financialDataFullyQualifiedFileName = settings[nameof(FinancialDataFullyQualifiedFileName)]?.ToString() ?? "";
					}

					public string FinancialDataFullyQualifiedFileName
					{
										get => _financialDataFullyQualifiedFileName;
										set
										{
															string oldValue = _financialDataFullyQualifiedFileName;
															try
															{
																				_financialDataFullyQualifiedFileName = value;
																				SaveToFile(nameof(FinancialDataFullyQualifiedFileName), value);
															}
															catch (Exception e)
															{
																				throw new SaveToFileException("Error saving configuration", e);
															}
															finally
															{
																				_financialDataFullyQualifiedFileName = oldValue;
															}
															ConfigChanged?.Invoke(this, EventArgs.Empty);
										}
					}

					public event EventHandler? ConfigChanged;

					private void SaveToFile(string node, string value)
					{
										string fileContent = File.ReadAllText(_fileName);
										JObject settings = JObject.Parse(fileContent);
										settings[node] = value;

										File.WriteAllText(_fileName, settings.ToString());
					}
}
