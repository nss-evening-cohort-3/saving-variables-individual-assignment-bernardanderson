using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingVariables
{
    class Program
    {
        static void Main(string[] args)
        {
            UserEntryData userEntryData = new UserEntryData();
            Expressions userExpressions = new Expressions();
            NonDatabaseCommands userNonDatabaseCommands = new NonDatabaseCommands();

            Console.WriteLine("Welcome to the variable saving program.");
            Console.WriteLine("Type 'help' for a list of valid commands.");

            while (userEntryData.WantsToExit == false)
            {
                Console.Write(">> ");
                userEntryData.EnteredUserString = Console.ReadLine();
                userEntryData = userExpressions.CheckExpressionTypeAndParse(userEntryData);

                if (userEntryData.ValidEntry)
                {
                    //Console.WriteLine($"Valid Command: { userEntryData.ValidEntry }");
                    //Console.WriteLine($"User Command: { userEntryData.UserCommand }");
                    //Console.WriteLine($"User UserVariableEqualsOrAll: { userEntryData.UserVariableEqualsOrAll }");
                    //Console.WriteLine($"User UserNumericValue: { userEntryData.UserNumericValue }");
                    userEntryData = userNonDatabaseCommands.RouteUserCommandToCorrectMethod(userEntryData);
                } else
                {
                    userEntryData.consoleOutputString = "Invalid Command";
                }

                Console.WriteLine(userEntryData.consoleOutputString);

                if (userEntryData.consoleOutputString == "Goodbye!")
                {
                    Console.ReadKey();
                    break;
                } else
                {
                    // Sets the 'lastq' command's data for every command except quit/exit
                    userEntryData.LastQ = userEntryData.EnteredUserString;
                }
            }
        }
    }
}
