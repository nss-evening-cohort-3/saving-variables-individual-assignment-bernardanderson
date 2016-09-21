namespace SavingVariables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Variables",
                c => new
                    {
                        VariablesId = c.Int(nullable: false, identity: true),
                        VariableName = c.String(nullable: false),
                        VariableValue = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VariablesId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Variables");
        }
    }
}
