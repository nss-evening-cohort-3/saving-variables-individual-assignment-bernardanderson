using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingVariables
{
    class NonDatabaseCommands
    {
        Dictionary<string, string> simpleUserCommands = new Dictionary<string, string>
        {
            { "lastq", "Prints the last entered command or expression" },
            { "exit", "Exits the program" },
            { "clear|quit", "Exits the program" },
            { "remove|show|delete", "Can be used to delete a variable (variable letter) or the entire database (all)" }
        };

        public UserEntryData RouteUserCommandToCorrectMethod(UserEntryData sentUserEntryData)
        {
            if (sentUserEntryData.UserCommand == "help")
            {
                sentUserEntryData.consoleOutputString = "This is help";
            }
            else if (sentUserEntryData.UserCommand == "exit" || sentUserEntryData.UserCommand == "quit")
            {
                sentUserEntryData.WantsToExit = true;
                sentUserEntryData.consoleOutputString = "Goodbye!";
            } else if (sentUserEntryData.UserCommand == "help")
            {
                sentUserEntryData.consoleOutputString = ReturnListOfCommandsWhenHelpIsEntered();
            }
            return sentUserEntryData;
        }

        public string ReturnListOfCommandsWhenHelpIsEntered()
        {
            string 

            return sentUserEntryData;
        }

    }
}
