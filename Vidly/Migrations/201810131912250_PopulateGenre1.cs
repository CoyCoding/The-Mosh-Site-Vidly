namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenre1 : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres VALUES ( 1, 'Comdey')");
            Sql("INSERT INTO Genres VALUES ( 2, 'Action')");
            Sql("INSERT INTO Genres VALUES ( 3, 'Thriller')");
            Sql("INSERT INTO Genres VALUES ( 4, 'Family')");
            Sql("INSERT INTO Genres VALUES ( 5, 'Romance')");
            Sql("INSERT INTO Genres VALUES ( 6, 'Horror')");
        }
        
        public override void Down()
        {
        }
    }
}
