using SavingVariables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SavingVariables
{
    public class VariablesContext : DbContext
    {
        // 'virtual' keyword is needed to use Moq during testing
        public virtual DbSet<Variables> VariablesTable { get; set; }
    }
}
