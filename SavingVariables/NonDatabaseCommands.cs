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
            { "lastq", "Prints the Last Entered Command or Expression" },
            { "exit|quit", "Exits the Program" },
            { "clear|delete|remove", "Used to delete a variable (variable letter) or the entire database (all)" },
            { "show all", "List all set variables in a tabular format" }
        };

        public UserEntryData RouteUserCommandToCorrectMethod(UserEntryData sentUserEntryData)
        {
            if (sentUserEntryData.UserCommand == "help")
            {
                sentUserEntryData.consoleOutputString = ReturnListOfCommandsWhenHelpIsEntered();
            }
            else if (sentUserEntryData.UserCommand == "exit" || sentUserEntryData.UserCommand == "quit")
            {
                sentUserEntryData.WantsToExit = true;
                sentUserEntryData.consoleOutputString = "Goodbye!";
            }
            return sentUserEntryData;
        }

        public string ReturnListOfCommandsWhenHelpIsEntered()
        {
            string helpCommandOutput = " **Current list of user commands**\n";
            List<string> tempKeyList = simpleUserCommands.Keys.ToList();
            tempKeyList.Sort();

            foreach (string key in tempKeyList)
            {
                helpCommandOutput += $"   {key}:  {simpleUserCommands[key]}\n";
            }
            return helpCommandOutput;
        }
    }
}
