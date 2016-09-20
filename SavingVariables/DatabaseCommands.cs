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
        public bool DeleteVariable(UserEntryData sentUserEntryDataForDelete)
        {
            //Check for presence of variable or 'all'
            // if there, delete it! 
            // if not, "Variable not set!"
            // sentUserEntryDataForDelete.UserVariable;
            return true;
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

                //The Context needs to add to the table Context.TableName.Add()
                Context.VariablesTable.Add(newVariable);
                Context.SaveChanges();
            }
            sentUserEntryDataForAdd.consoleOutputString = $"{sentUserEntryDataForAdd.UserCommand} = {sentUserEntryDataForAdd.UserNumericValue} added to database";
            return sentUserEntryDataForAdd;
        }

        public bool ReturnAllVariableEqualities(UserEntryData sentUserEntryForShowAll)
        {
            // Database pull for showing all variable relationships
            return true;
        }
    }
}
