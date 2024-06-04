// See https://aka.ms/new-console-template for more information
using TextInterpreterButCMD;

string text = "";
do
{
					Console.WriteLine("Type exit to leave");
					Console.WriteLine("Insert transaction:");
					text = Console.ReadLine() ?? "exit";
					Transaction transaction = TextInterpreter.InterpretText(text);
					TransactionSaver.Save(transaction);
					Console.Clear();
} while (text.ToLower() != "exit");