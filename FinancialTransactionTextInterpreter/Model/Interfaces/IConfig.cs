﻿namespace FinancialTransactionTextInterpreter.Model.Interfaces;
public interface IConfig
{
					event EventHandler? ConfigChanged;

					string FinancialDataFullyQualifiedFileName { get; set; }
					string ApplicationLanguage { get; set; }
}
