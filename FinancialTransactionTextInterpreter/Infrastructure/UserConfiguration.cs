using FinancialTransactionTextInterpreter.Model.Exceptions;
using FinancialTransactionTextInterpreter.Model.Interfaces;
using Newtonsoft.Json.Linq;
using System.IO;

namespace FinancialTransactionTextInterpreter.Infrastructure;
public class UserConfiguration : IConfig
{
					//TODO: Extract file logic to separate class
					private string _fileName = "";
					private string _financialDataFullyQualifiedFileName = "";
					private string _applicationLanguage = "";

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
										_applicationLanguage = settings[nameof(ApplicationLanguage)]?.ToString() ?? "en-US";
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
																				_financialDataFullyQualifiedFileName = oldValue;
																				throw new SaveToFileException("Error saving configuration", e);
															}
															ConfigChanged?.Invoke(this, EventArgs.Empty);
										}
					}

					public string ApplicationLanguage
					{
										get => _applicationLanguage;
										set
										{
															_applicationLanguage = value;
															try
															{
																				SaveToFile(nameof(ApplicationLanguage), value);
															}
															catch (Exception e)
															{
																				throw new SaveToFileException("Error saving configuration", e);
															}
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
