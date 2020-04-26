namespace Task5_EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Northwind.CreditCards",
                c => new
                    {
                        CardNumber = c.String(nullable: false, maxLength: 16),
                        ExpireDate = c.DateTime(nullable: false, storeType: "date"),
                        CardHolder = c.String(nullable: false),
                        EmployeeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CardNumber)
                .ForeignKey("Northwind.Employees", t => t.EmployeeID, cascadeDelete: true)
                .Index(t => t.EmployeeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Northwind.CreditCards", "EmployeeID", "Northwind.Employees");
            DropIndex("Northwind.CreditCards", new[] { "EmployeeID" });
            DropTable("Northwind.CreditCards");
        }
    }
}
