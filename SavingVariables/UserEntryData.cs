using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingVariables
{
    // Simply a class to allow the passing of fields between other classes
    public class UserEntryData
    {
        public bool ValidEntry { get; set; } = false;
        public bool WantsToExit { get; set; } = false;
        public string EnteredUserString { get; set; } = "blank";
        public string UserCommand { get; set; } = "blank";
        public string UserVariableEqualsOrAll { get; set; } = "blank";
        public string UserNumericValue { get; set; } = "blank";
        public string consoleOutputString { get; set; } = "**Blank Console Output**";
    }
}
