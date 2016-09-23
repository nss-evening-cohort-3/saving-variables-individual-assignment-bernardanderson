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

        /// 
        ///  Start of tests for Expressions Class
        /// 
        [TestMethod]
        public void Exp_CanIMakeAnInstanceOfExpressions()
        {
            Expressions testExpressions = new Expressions();
            Assert.IsNotNull(testExpressions);
        }

        [TestMethod]
        public void Exp_CanRegExCatchAllCommands()
        {
            Expressions test_Expression_Instance = new Expressions();

            UserEntryData test_UserEntryData_Constant = new UserEntryData { EnteredUserString = "x=2" };
            UserEntryData test_UserEntryData_Exit = new UserEntryData { EnteredUserString = "exit" };
            UserEntryData test_UserEntryData_Quit = new UserEntryData { EnteredUserString = "quit" };
            UserEntryData test_UserEntryData_Help = new UserEntryData { EnteredUserString = "help" };
            UserEntryData test_UserEntryData_ClearVariable = new UserEntryData { EnteredUserString = "clear a" };
            UserEntryData test_UserEntryData_RemoveVariable = new UserEntryData { EnteredUserString = "remove a" };
            UserEntryData test_UserEntryData_DeleteVariable = new UserEntryData { EnteredUserString = "delete a" };
            UserEntryData test_UserEntryData_ClearAll = new UserEntryData { EnteredUserString = "clear all" };
            UserEntryData test_UserEntryData_RemoveAll = new UserEntryData { EnteredUserString = "remove all" };
            UserEntryData test_UserEntryData_DeleteAll = new UserEntryData { EnteredUserString = "delete all" };
            UserEntryData test_UserEntryData_ShowAll = new UserEntryData { EnteredUserString = "show all" };
            UserEntryData test_UserEntryData_Lastq = new UserEntryData { EnteredUserString = "lastq" };
            UserEntryData test_UserEntryData_NotValidCommand = new UserEntryData { EnteredUserString = "flabby" };

            Assert.IsTrue(test_Expression_Instance.CheckExpressionTypeAndParse(test_UserEntryData_Constant).ValidEntry);
            Assert.AreEqual("x", test_Expression_Instance.CheckExpressionTypeAndParse(test_UserEntryData_Constant).UserVariable);

            Assert.IsTrue(test_Expression_Instance.CheckExpressionTypeAndParse(test_UserEntryData_Exit).ValidEntry);
            Assert.AreEqual("exit", test_Expression_Instance.CheckExpressionTypeAndParse(test_UserEntryData_Exit).UserCommand);

            Assert.IsTrue(test_Expression_Instance.CheckExpressionTypeAndParse(test_UserEntryData_Quit).ValidEntry);
            Assert.AreEqual("quit", test_Expression_Instance.CheckExpressionTypeAndParse(test_UserEntryData_Quit).UserCommand);

            Assert.IsTrue(test_Expression_Instance.CheckExpressionTypeAndParse(test_UserEntryData_Help).ValidEntry);
            Assert.AreEqual("help", test_Expression_Instance.CheckExpressionTypeAndParse(test_UserEntryData_Help).UserCommand);

            Assert.IsTrue(test_Expression_Instance.CheckExpressionTypeAndParse(test_UserEntryData_ClearVariable).ValidEntry);
            Assert.AreEqual("clear", test_Expression_Instance.CheckExpressionTypeAndParse(test_UserEntryData_ClearVariable).UserCommand);
            Assert.AreEqual("a", test_Expression_Instance.CheckExpressionTypeAndParse(test_UserEntryData_ClearVariable).UserVariable);

            Assert.IsTrue(test_Expression_Instance.CheckExpressionTypeAndParse(test_UserEntryData_RemoveVariable).ValidEntry);
            Assert.AreEqual("remove", test_Expression_Instance.CheckExpressionTypeAndParse(test_UserEntryData_RemoveVariable).UserCommand);
            Assert.AreEqual("a", test_Expression_Instance.CheckExpressionTypeAndParse(test_UserEntryData_RemoveVariable).UserVariable);

            Assert.IsTrue(test_Expression_Instance.CheckExpressionTypeAndParse(test_UserEntryData_DeleteVariable).ValidEntry);
            Assert.AreEqual("delete", test_Expression_Instance.CheckExpressionTypeAndParse(test_UserEntryData_DeleteVariable).UserCommand);
            Assert.AreEqual("a", test_Expression_Instance.CheckExpressionTypeAndParse(test_UserEntryData_DeleteVariable).UserVariable);

            Assert.IsTrue(test_Expression_Instance.CheckExpressionTypeAndParse(test_UserEntryData_ClearAll).ValidEntry);
            Assert.AreEqual("clear", test_Expression_Instance.CheckExpressionTypeAndParse(test_UserEntryData_ClearAll).UserCommand);
            Assert.AreEqual("all", test_Expression_Instance.CheckExpressionTypeAndParse(test_UserEntryData_ClearAll).UserVariable);

            Assert.IsTrue(test_Expression_Instance.CheckExpressionTypeAndParse(test_UserEntryData_RemoveAll).ValidEntry);
            Assert.AreEqual("remove", test_Expression_Instance.CheckExpressionTypeAndParse(test_UserEntryData_RemoveAll).UserCommand);
            Assert.AreEqual("all", test_Expression_Instance.CheckExpressionTypeAndParse(test_UserEntryData_RemoveAll).UserVariable);

            Assert.IsTrue(test_Expression_Instance.CheckExpressionTypeAndParse(test_UserEntryData_DeleteAll).ValidEntry);
            Assert.AreEqual("delete", test_Expression_Instance.CheckExpressionTypeAndParse(test_UserEntryData_DeleteAll).UserCommand);
            Assert.AreEqual("all", test_Expression_Instance.CheckExpressionTypeAndParse(test_UserEntryData_DeleteAll).UserVariable);

            Assert.IsTrue(test_Expression_Instance.CheckExpressionTypeAndParse(test_UserEntryData_ShowAll).ValidEntry);
            Assert.AreEqual("show all", test_Expression_Instance.CheckExpressionTypeAndParse(test_UserEntryData_ShowAll).UserCommand);

            Assert.IsTrue(test_Expression_Instance.CheckExpressionTypeAndParse(test_UserEntryData_Lastq).ValidEntry);
            Assert.AreEqual("lastq", test_Expression_Instance.CheckExpressionTypeAndParse(test_UserEntryData_Lastq).UserCommand);

            Assert.IsFalse(test_Expression_Instance.CheckExpressionTypeAndParse(test_UserEntryData_NotValidCommand).ValidEntry);
        }
        /// 
        /// End of Expressions Method Tests
        /// 

        /// 
        /// Start of tests for NonDatabaseCommands Class
        /// 
        [TestMethod]
        public void NonDBCom_CanIMakeAnInstanceOfNonDatabaseCommands()
        {
            NonDatabaseCommands testNonDatabaseCommands = new NonDatabaseCommands();

            Assert.IsNotNull(testNonDatabaseCommands);
        }

        [TestMethod]
        public void NonDBCom_CanHelpListBeReturned()
        {
            NonDatabaseCommands testNonDatabaseCommands = new NonDatabaseCommands();

            Assert.IsInstanceOfType(testNonDatabaseCommands.ReturnListOfCommandsWhenHelpIsEntered(), typeof(String));  
        }

        [TestMethod]
        public void NonDBCom_DoesDefaultInputReturnError()
        {
            NonDatabaseCommands testNonDatabaseCommands = new NonDatabaseCommands();

            UserEntryData test_UserEntryData_WrongInput = new UserEntryData { EnteredUserString = "bleep" };

            Assert.AreEqual("Invalid Entry!!", testNonDatabaseCommands.RouteUserCommandToCorrectMethod(test_UserEntryData_WrongInput).consoleOutputString);
        }

        /// 
        /// End of tests for NonDatabaseCommands Class
        ///

        /// 
        /// Start of tests for DatabaseCommands Class
        /// 
        [TestMethod]
        public void DbComm_CanIMakeAnInstanceOfDatabaseCommands()
        {
            DatabaseCommands testDatabaseCommands = new DatabaseCommands();

            Assert.IsNotNull(testDatabaseCommands);
        }

        [TestMethod]
        public void DbComm_EnsureRepoHasContext()
        {
            DatabaseCommands repo = new DatabaseCommands();
            VariablesContext actual_context = repo.Context;
            Assert.IsInstanceOfType(actual_context, typeof(VariablesContext));
        }

        [TestMethod]
        public void DbComm_EnsureWeHaveNoVariables()
        {
            // Arrange

            // Act
            UserEntryData test_UserEntryData = new UserEntryData { ValidEntry = true }; // Default set to true, should change to false
            Boolean resultOfNoVariables = repo.IsVariableAlreadyPresentForAdd(test_UserEntryData);

            // Assert
            Assert.IsFalse(resultOfNoVariables);
        }

        [TestMethod]
        public void DbComm_EnsureVariableCanBeAddedToDatabase()
        {
            //Arrange
            UserEntryData test_UserEntryDataForA = new UserEntryData { UserCommand="add", UserVariable="a", UserNumericValue="2", ValidEntry = true }; // Default set to true, should change to false
            UserEntryData test_UserEntryDataForB = new UserEntryData { UserCommand="add", UserVariable = "b", UserNumericValue = "2", ValidEntry = true }; // Default set to true, should change to false

            //Act
            repo.AddVariable(test_UserEntryDataForA); // Only 'a' is added
            Boolean variable_presentA = repo.IsVariableAlreadyPresentForAdd(test_UserEntryDataForA);
            Boolean variable_presentB = repo.IsVariableAlreadyPresentForAdd(test_UserEntryDataForB);

            //Assert
            Assert.IsTrue(variable_presentA); // If 'a' was added, it should be reported as 'true'
            Assert.IsFalse(variable_presentB); // 'b' wasn't added, so it should be reported as 'false'
        }

        [TestMethod]
        public void DbComm_EnsureVariableCanBeDeletedFromDatabase()
        {
            //Arrange
            UserEntryData test_UserEntryDataForA = new UserEntryData { UserCommand = "add", UserVariable = "a", UserNumericValue = "2", ValidEntry = true }; // Default set to true, should change to false

            //Act
            repo.AddVariable(test_UserEntryDataForA); // 'a' is added
            Boolean variable_presentBeforeDelete = repo.IsVariableAlreadyPresentForRemove(test_UserEntryDataForA); //Checked for presence in Database
            repo.DeleteVariables(test_UserEntryDataForA); // Removed from database
            Boolean variable_presentAfterDelete = repo.IsVariableAlreadyPresentForRemove(test_UserEntryDataForA);  //Checked for presence in Database

            //Assert
            Assert.IsTrue(variable_presentBeforeDelete); // If 'a' was added, it should be reported as 'true'
            Assert.IsFalse(variable_presentAfterDelete); // If 'a' was deleted, it should be reported as 'true'
        }
        /// 
        /// End of tests for DatabaseCommands Class
        /// 

    }
}
