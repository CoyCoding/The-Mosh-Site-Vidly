namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRentalInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RentalInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateRented = c.DateTime(nullable: false),
                        DateReturned = c.DateTime(),
                        Customer_Id = c.Int(),
                        Movie_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .ForeignKey("dbo.Movies", t => t.Movie_Id)
                .Index(t => t.Customer_Id)
                .Index(t => t.Movie_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RentalInfoes", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.RentalInfoes", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.RentalInfoes", new[] { "Movie_Id" });
            DropIndex("dbo.RentalInfoes", new[] { "Customer_Id" });
            DropTable("dbo.RentalInfoes");
        }
    }
}
