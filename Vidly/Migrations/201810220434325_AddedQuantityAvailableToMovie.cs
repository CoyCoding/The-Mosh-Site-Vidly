namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedQuantityAvailableToMovie : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "QuantityAvailable", c => c.Int(nullable: false));

            Sql("UPDATE Movies SET QuantityAvailable = QuantityInStock");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "QuantityAvailable");
        }
    }
}
