namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class poo : DbMigration
    {
       
                  public override void Up()
        {
            CreateTable(
    "dbo.Movies",
    c => new
    {
        Id = c.Int(nullable: false, identity: true),
        Name = c.String(nullable: false, maxLength: 255),
        ReleaseDate = c.DateTime(nullable: false),
        DateAdded = c.DateTime(nullable: false),
        QuantityInStock = c.Int(nullable: false),
        Genre_Id = c.Int(nullable: false),
        GenreId_Id = c.Int(),
    })
    .PrimaryKey(t => t.Id)
    .ForeignKey("dbo.Genres", t => t.Genre_Id, cascadeDelete: true)
    .ForeignKey("dbo.Genres", t => t.GenreId_Id)
    .Index(t => t.Genre_Id)
    .Index(t => t.GenreId_Id);

            CreateTable(
                "dbo.Genres",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 255),
                })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
        }
    }
}
