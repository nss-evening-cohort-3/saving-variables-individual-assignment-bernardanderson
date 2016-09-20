using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SavingVariables.Models;
using System.Threading.Tasks;

namespace SavingVariables
{
    class DatabaseCommands
    {
        // Checks for the presence of a variable in the database 
        public bool IsVariableAlreadyPresent(UserEntryData sentUserEntryDataForAdd)
        {
            // Using 'using (context){}' makes sure the database closes after saving
            using (VariablesContext Context = new VariablesContext())
            {
                // The .Where() is using LINQ syntax to search the database for what variable the user had entered
                //  If the variable is found then the .Count != 0
                int variableFound = Context.VariablesTable.Where(b => b.VariableName == sentUserEntryDataForAdd.UserCommand).Count();
                return (variableFound == 0) ? false : true;
            }
        }
        public string DeleteVariable(UserEntryData sentUserEntryDataForDelete)
        {
            // Using 'using (context){}' makes sure the database closes after saving
            using (VariablesContext Context = new VariablesContext())
            {
                // LINQ query that looks in table VariableName for UserVariable and selects the first instance of it (there should only be one
                //  anyway.
                Variables variableItemToBeDeleted = Context.VariablesTable.Where(v => v.VariableName == sentUserEntryDataForDelete.UserVariable).First();
                Context.VariablesTable.Remove(variableItemToBeDeleted);
                Context.SaveChanges();
            }
            return $"The {sentUserEntryDataForDelete.UserVariable} variable has been removed!";
        }

        public UserEntryData AddVariable(UserEntryData sentUserEntryDataForAdd)
        {
            // Using 'using (context){}' makes sure the database closes after saving
            using (VariablesContext Context = new VariablesContext())
            {
                Variables newVariable = new Variables()
                {
                    VariableName = sentUserEntryDataForAdd.UserCommand,
                    VariableValue = Convert.ToInt32(sentUserEntryDataForAdd.UserNumericValue)
                };
                // The Context needs to add to the table via Context.TableName.Add()
                Context.VariablesTable.Add(newVariable);
                Context.SaveChanges();
            }
            sentUserEntryDataForAdd.consoleOutputString = $"{sentUserEntryDataForAdd.UserCommand} = {sentUserEntryDataForAdd.UserNumericValue} added to database";
            return sentUserEntryDataForAdd;
        }
        // Pretty-prints all database variables into a table
        public string ReturnAllVariableEqualities(UserEntryData sentUserEntryForShowAll)
        {
            string tableListOfVariables = "";
            // Using 'using (context){}' makes sure the database closes after saving
            using (VariablesContext Context = new VariablesContext())
            {
                tableListOfVariables = " ----------------- \n";
                tableListOfVariables += "|  Name  |  Value |\n";
                tableListOfVariables += " ----------------- \n";

                // Database pull for showing all variable relationships
                var databaseVariables = Context.VariablesTable;
                foreach (var databaseLine in databaseVariables)
                {
                    tableListOfVariables += $"|   {databaseLine.VariableName}    |    {databaseLine.VariableValue}   |\n";
                }

                tableListOfVariables += " ----------------- \n";
            }
            return tableListOfVariables;
        }
    }
}
