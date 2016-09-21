using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SavingVariables.Models
{
    public class Variables
    {
        [Key]
        public int VariablesId { get; set; }
        [Required]
        public string VariableName { get; set; }
        [Required]
        public int VariableValue { get; set; }
    }
}
