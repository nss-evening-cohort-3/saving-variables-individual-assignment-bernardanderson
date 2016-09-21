using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SavingVariables.Models;
using System.Threading.Tasks;

namespace SavingVariables
{
    public class DatabaseCommands
    {
        public VariablesContext Context { get; set; } // Dependancy Injection

        public DatabaseCommands()
        {
            Context = new VariablesContext();
        }

        public DatabaseCommands(VariablesContext _context) // Dependancy Injection
        {                                           // Dependancy Injection
            Context = _context;                     // Dependancy Injection
        }

        // Checks for the presence of a variable in the database 
        public bool IsVariableAlreadyPresentForAdd(UserEntryData sentUserEntryDataForAdd)
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

        // Checks for the presence of a variable in the database 
        public bool IsVariableAlreadyPresentForRemove(UserEntryData sentUserEntryDataForRemove)
        {
            using (VariablesContext Context = new VariablesContext())
            {
                int variableFound = Context.VariablesTable.Where(b => b.VariableName == sentUserEntryDataForRemove.UserVariable).Count();
                return (variableFound != 0 || sentUserEntryDataForRemove.UserVariable == "all") ? true : false;
            }
        }

        // Deletes a single variable from the database
        public string DeleteVariables(UserEntryData sentUserEntryDataForDelete)
        {
            string outputString = "";
            
            using (VariablesContext Context = new VariablesContext())
            {
                if (sentUserEntryDataForDelete.UserVariable == "all")
                {
                    var removeAllDatabaseVariables = Context.VariablesTable;
                    Context.VariablesTable.RemoveRange(removeAllDatabaseVariables);
                    outputString = $"    The entire database has been cleared!";
                }
                else
                {
                    // LINQ query that looks in table VariableName for UserVariable and selects the first instance (there should only be one anyway).
                    Variables variableItemToBeDeleted = Context.VariablesTable.Where(v => v.VariableName == sentUserEntryDataForDelete.UserVariable).First();
                    outputString = $"    Variable '{sentUserEntryDataForDelete.UserVariable}' has been deleted!";
                    Context.VariablesTable.Remove(variableItemToBeDeleted);
                }
                Context.SaveChanges();
            }
            return outputString;
        }

        // Adds a variable to the database
        public UserEntryData AddVariable(UserEntryData sentUserEntryDataForAdd)
        {
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
            sentUserEntryDataForAdd.consoleOutputString = $"   Saved '{sentUserEntryDataForAdd.UserCommand}' as '{sentUserEntryDataForAdd.UserNumericValue}'";
            return sentUserEntryDataForAdd;
        }

        // Pretty-prints all database variables into a table
        public string ReturnAllVariableEqualities(UserEntryData sentUserEntryForShowAll)
        {
            string tableListOfVariables = " ----------------- \n";
            tableListOfVariables += "|  Name  |  Value |\n";
            tableListOfVariables += " ----------------- \n";
            
            using (VariablesContext Context = new VariablesContext())
            {
                // Database pull for showing all variable relationships
                var databaseVariables = Context.VariablesTable;
                foreach (var databaseLine in databaseVariables)
                {
                    tableListOfVariables += $"|   {databaseLine.VariableName}    |    {databaseLine.VariableValue}   |\n";
                }
            }
            tableListOfVariables += " ----------------- \n";
            return tableListOfVariables;
        }
    }
}
