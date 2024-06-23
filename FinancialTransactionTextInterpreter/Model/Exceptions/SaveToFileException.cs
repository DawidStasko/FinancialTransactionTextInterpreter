namespace FinancialTransactionTextInterpreter.Model.Exceptions;

[Serializable]
public class SaveToFileException : Exception
{
					public SaveToFileException() { }
					public SaveToFileException(string message) : base(message) { }
					public SaveToFileException(string message, Exception inner) : base(message, inner) { }
					protected SaveToFileException(
				System.Runtime.Serialization.SerializationInfo info,
				System.Runtime.Serialization.StreamingContext context)
					{ }
}