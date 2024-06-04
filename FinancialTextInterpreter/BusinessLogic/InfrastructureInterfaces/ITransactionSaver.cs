using FinancialTextInterpreter.Domain;

namespace FinancialTextInterpreter.BusinessLogic.InfrastructureInterfaces;

public interface ITransactionSaver
{
					void Save(Transaction transaction);
}
