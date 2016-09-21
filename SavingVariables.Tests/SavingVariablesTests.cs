using System;
using System.Text;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SavingVariables.Models;

namespace SavingVariables.Tests
{
    [TestClass]
    public class SavingVariablesTests
    {
        // Create Moq Block Context
        Mock<VariablesContext> mock_VariablesContext { get; set; }
        Mock<DbSet<Variables>> mock_variable_table { get; set; }
        List<Variables> mock_variable_list { get; set; } // Fake list to act as table
        DatabaseCommands repo { get; set; }

        // Utility Method
        public void ConnectMocksToDatastore()
        {
            var queryable_list = mock_variable_list.AsQueryable();
            // Make LINQ think that our new Oueryable List is a Database table.
            mock_variable_table.As<IQueryable<Variables>>().Setup(m => m.Provider).Returns(queryable_list.Provider);
            mock_variable_table.As<IQueryable<Variables>>().Setup(m => m.Expression).Returns(queryable_list.Expression);
            mock_variable_table.As<IQueryable<Variables>>().Setup(m => m.ElementType).Returns(queryable_list.ElementType);
            mock_variable_table.As<IQueryable<Variables>>().Setup(m => m.GetEnumerator()).Returns(() => queryable_list.GetEnumerator());

            // Have our Author property return our Queryable List AKA fake database table.
            mock_VariablesContext.Setup(c => c.VariablesTable).Returns(mock_variable_table.Object);
        
            // How to define a Callback in response to a called method.
            mock_variable_table.Setup(t => t.Add(It.IsAny<Variables>())).Callback((Variables a) => mock_variable_list.Add(a));
            mock_variable_table.Setup(t => t.Remove(It.IsAny<Variables>())).Callback((Variables a) => mock_variable_list.Remove(a));
        }

        [TestInitialize]
        public void Initialize()
        {
            // Create Moq Block Context
            mock_VariablesContext = new Mock<VariablesContext>();
            mock_variable_table = new Mock<DbSet<Variables>>();
            mock_variable_list = new List<Variables>();  // Fake list to act as table
            ConnectMocksToDatastore();
            repo = new DatabaseCommands(mock_VariablesContext.Object);
        }
 
        [TestCleanup]
        public void TearDown()
        {
            repo = null; //Removes the created BlogContext Instance
        }

        [TestMethod]
        public void CanIMakeAnInstanceOfExpressions()
        {
            Expressions testExpressions = new Expressions();

            Assert.IsNotNull(testExpressions);
        }
        [TestMethod]
        public void CanIMakeAnInstanceOfDatabaseCommands()
        {
            DatabaseCommands testDatabaseCommands = new DatabaseCommands();

            Assert.IsNotNull(testDatabaseCommands);
        }
        [TestMethod]
        public void CanIMakeAnInstanceOfNonDatabaseCommands()
        {
            NonDatabaseCommands testNonDatabaseCommands = new NonDatabaseCommands();

            Assert.IsNotNull(testNonDatabaseCommands);
        }
    }
}
