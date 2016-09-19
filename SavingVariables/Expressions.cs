using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingVariables
{
    public class Expressions
    {
        string[] regularExpressions = new string[] {
            @"^\s*([a-zA-Z])\s*(=)\s*([-|\+]{0,1}\d+)\s*$",       // Constant Equals Number
            @"^\s*(quit)\s*$",                                    // For exit
            @"^\s*(exit)\s*$",                                    // For quit
            @"^\s*(help)\s*$",                                    // For help
            @"^\s*(clear)\s*([a-zA-Z])\s*$",                      // For clear
            @"^\s*(clear)\s*(all)\s*$",                           // For clear all
            @"^\s*(remove)\s*([a-zA-Z])\s*$",                     // For remove
            @"^\s*(remove)\s*(all)\s*$",                          // For remove all
            @"^\s*(delete)\s*([a-zA-Z])\s*$",                     // For delete
            @"^\s*(delete)\s*(all)\s*$",                          // For delete all
            @"^\s*(show all)\s*$"                                 // For show all
        };

        // This cycles through the RegEx's in the array above to see if a match can be made against the user entered command.
        //  The userInput is parsed or not and the UserEntryData class is returned.
        public UserEntryData CheckExpressionTypeAndParse(UserEntryData sentUserInputFromCommandPrompt)
        {
            for (int i = 0; i < regularExpressions.Length; i++)
            {
                if (new Regex(regularExpressions[i]).IsMatch(sentUserInputFromCommandPrompt.EnteredUserString.ToLower())) // Cycles through all Regular Expression for a match  
                {
                    Match matchedFields = new Regex(regularExpressions[i]).Match(sentUserInputFromCommandPrompt.EnteredUserString);
                    sentUserInputFromCommandPrompt.ValidEntry = true;
                    sentUserInputFromCommandPrompt.UserCommand = matchedFields.Groups[1].ToString();
                    sentUserInputFromCommandPrompt.UserVariableEqualsOrAll = matchedFields.Groups[2].ToString();
                    sentUserInputFromCommandPrompt.UserNumericValue = matchedFields.Groups[3].ToString();

                    return sentUserInputFromCommandPrompt;
                }
            }
            sentUserInputFromCommandPrompt.ValidEntry = false;
            return sentUserInputFromCommandPrompt;
        }
    }
}
