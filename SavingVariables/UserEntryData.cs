using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingVariables
{
    // Simply a class to allow the passing of user entry properties between classes
    public class UserEntryData
    {
        public bool ValidEntry { get; set; } = false;
        public bool WantsToExit { get; set; } = false;
        public string EnteredUserString { get; set; } = "";
        public string UserCommand { get; set; } = "";
        public string LastQ { get; set; } = "None Entered";
        public string UserVariable { get; set; } = "";
        public string UserNumericValue { get; set; } = "";
        public string consoleOutputString { get; set; } = "";
    }
}
