namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMembershipName : DbMigration
    {
        public override void Up()
        {
            Sql("Update MembershipTypes SET name = 'Pay as You Go' WHERE Id = 1");
            Sql("Update MembershipTypes SET name = 'Monthly' WHERE Id = 2");
            Sql("Update MembershipTypes SET name = 'Quarterly' WHERE Id = 3");
            Sql("Update MembershipTypes SET name = 'Annualy' WHERE Id = 4");
        }
        
        public override void Down()
        {
        }
    }
}
