using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public bool AddVariable(UserEntryData sentUserEntryDataForDelete)
        {
            //Check for presence of variable
            // if there, "Variable already set!" 
            // if not, set it!
            // sentUserEntryDataForDelete.UserVariable;
            // sentUserEntryDataForDelete.UserNumericValue;
            return true;
        }

        public bool ReturnAllVariableEqualities(UserEntryData sentUserEntryForShowAll)
        {
            // Database pull for showing all variable relationships
            return true;
        }
    }
}
