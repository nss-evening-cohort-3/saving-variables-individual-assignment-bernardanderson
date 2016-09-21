using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingVariables
{
    public class NonDatabaseCommands
    {
        DatabaseCommands userDatabaseCommands = new DatabaseCommands();

        Dictionary<string, string> simpleUserCommands = new Dictionary<string, string>
        {
            { "lastq", "Prints the Last Entered Command or Expression" },
            { "exit|quit", "Exits the Program" },
            { "clear|delete|remove", "Used to delete a variable (variable letter) or the entire database (all)" },
            { "show all", "List all set variables in a tabular format" }
        };

        public UserEntryData RouteUserCommandToCorrectMethod(UserEntryData sentUserEntryData)
        {
            switch (sentUserEntryData.UserCommand)
            {
                case ("show all"):
                    sentUserEntryData.consoleOutputString = userDatabaseCommands.ReturnAllVariableEqualities(sentUserEntryData);
                    break;
                case ("help"):
                    sentUserEntryData.consoleOutputString = ReturnListOfCommandsWhenHelpIsEntered();
                    break;
                case ("exit"):
                case ("quit"):
                    sentUserEntryData.WantsToExit = true;
                    sentUserEntryData.consoleOutputString = "Goodbye!";
                    break;
                case ("clear"):
                case ("delete"):
                case ("remove"):
                    if (userDatabaseCommands.IsVariableAlreadyPresentForRemove(sentUserEntryData))
                    {
                        sentUserEntryData.consoleOutputString = userDatabaseCommands.DeleteVariables(sentUserEntryData);
                    }
                    else
                    {
                        sentUserEntryData.consoleOutputString = $"Error! {sentUserEntryData.UserVariable} is not defined!";
                    }
                    break;
                case ("lastq"):
                    sentUserEntryData.consoleOutputString = sentUserEntryData.LastQ;
                    break;
                default: //The default has to be the setting/adding of a variable
                    if (userDatabaseCommands.IsVariableAlreadyPresentForAdd(sentUserEntryData))
                    {
                        sentUserEntryData.consoleOutputString = $"Error! {sentUserEntryData.UserCommand} is already defined!";
                    } else
                    {
                        sentUserEntryData = userDatabaseCommands.AddVariable(sentUserEntryData);
                    }
                    break;
            }
            return sentUserEntryData;
        }
        
        public string ReturnListOfCommandsWhenHelpIsEntered()
        {
            string helpCommandOutput = " **List of possible user commands**\n";
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
