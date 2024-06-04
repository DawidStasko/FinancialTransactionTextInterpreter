using FinancialTextInterpreter.BusinessLogic.InfrastructureInterfaces;
using FinancialTextInterpreter.Domain;

namespace FinancialTextInterpreter.BusinessLogic;

public class ProceedTypedInTransactions
{
					private readonly ITransactionSaver _transactionSaver;

					public ProceedTypedInTransactions(ITransactionSaver transactionSaver)
					{
										_transactionSaver = transactionSaver;
					}

					public void Proceed(IEnumerable<string> transactionsTexts)
					{
										IList<Transaction> transactions = [];
										foreach (string text in transactionsTexts)
										{
															Transaction transaction = TextInterpreter.InterpretText(text);
															transactions.Add(transaction);
										}

										foreach (Transaction transaction in transactions)
										{
															_transactionSaver.Save(transaction);
										}
					}
}
