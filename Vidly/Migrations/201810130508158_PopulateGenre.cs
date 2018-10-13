namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenre : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres Values('Comedy')");
            Sql("INSERT INTO Genres Values('Action')");
            Sql("INSERT INTO Genres Values('Thriller')");
            Sql("INSERT INTO Genres Values('Family')");
            Sql("INSERT INTO Genres Values('Romance')");
            Sql("INSERT INTO Genres Values('Horror')");

        }
        
        public override void Down()
        {

        }
    }
}
