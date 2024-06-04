using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AvaloniaPromptInterpreter.Models;
public class TypedInTransaction : ObservableObject
{
					private readonly Guid _guid = new();
					private string _transactionText = "";

					public Guid Guid => _guid;
					public string TransactionText
					{
										get => _transactionText;
										set
										{
															_transactionText = value;
															OnPropertyChanged();
										}
					}
					public TypedInTransaction(string transactionText)
					{
										TransactionText = transactionText;
					}
}
