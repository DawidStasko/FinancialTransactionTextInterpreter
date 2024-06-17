namespace FinancialTransactionTextInterpreter.Model;

public record Result<T>()
{
					public bool IsSuccess
					{
										get
										{
															if (ErrorMessages == null)
																				return true;
															return ErrorMessages.Count == 0;
										}
					}
					public T? Value { get; init; }
					public IReadOnlyCollection<string> ErrorMessages { get; init; }
}
