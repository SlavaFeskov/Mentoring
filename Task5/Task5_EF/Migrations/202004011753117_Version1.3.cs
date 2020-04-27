namespace Task5_EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version13 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "Northwind.Region", newName: "Regions");
            AddColumn("Northwind.Customers", "CreationDate", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            DropColumn("Northwind.Customers", "CreationDate");
            RenameTable(name: "Northwind.Regions", newName: "Region");
        }
    }
}
