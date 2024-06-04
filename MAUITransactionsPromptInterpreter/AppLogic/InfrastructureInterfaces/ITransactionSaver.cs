using MAUITransactionsPromptInterpreter.Domain;
namespace MAUITransactionsPromptInterpreter.AppLogic.InfrastructureInterfaces;

public interface ITransactionSaver
{
					void Save(Transaction transaction);
}
