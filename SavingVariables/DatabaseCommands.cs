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
        public bool DeleteVariable(UserEntryData sentUserEntryDataForDelete)
        {
            //Check for presence of variable or 'all'
            // if there, delete it! 
            // if not, "Variable not set!"
            // sentUserEntryDataForDelete.UserVariable;
            return true;
        }
        public bool AddVariable(UserEntryData sentUserEntryDataForAdd)
        {
            using (VariablesContext context = new VariablesContext())
            {
                Variables newVariable = new Variables()
                {
                    VariableName = sentUserEntryDataForAdd.UserVariable,
                    VariableValue = Convert.ToInt32(sentUserEntryDataForAdd.UserNumericValue)
                };

                //The Context needs to add to the table context.TableName.Add
                context.VariablesTable.Add(newVariable);
                context.SaveChanges();
            }

            return true;
        }

        public bool ReturnAllVariableEqualities(UserEntryData sentUserEntryForShowAll)
        {
            // Database pull for showing all variable relationships
            return true;
        }
    }
}
