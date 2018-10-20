namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserData : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'204c57d5-5c34-4736-85e1-3835829f1647', N'guest@palms.com', 0, N'AEhKq6LSzsCOJFGgYm0dwFc+XAApjkpI1H+eyRXz8rS9UcKsagRpm0O4h8db0MBW9w==', N'2438d1e1-01bc-4721-965e-6aa0698d825a', NULL, 0, 0, NULL, 1, 0, N'guest@palms.com')
                  INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'5bdb1b12-c79e-494d-a8b1-158eba69ea97', N'admin@palms.com', 0, N'AHw6pk1XtmJz4Db+ED/zdFcYRxyz1wSMGyzv4FXnOnnEoomKKHw1JQfJGUQRHj9dPg==', N'49df0ec1-42c7-4b74-980e-5d7340cac312', NULL, 0, 0, NULL, 1, 0, N'admin@palms.com')
                  INSERT INTO[dbo].[AspNetRoles]([Id], [Name]) VALUES(N'bfa9fa57-da37-461d-a035-aec120870bc1', N'CanManageMovies')
                  INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'5bdb1b12-c79e-494d-a8b1-158eba69ea97', N'bfa9fa57-da37-461d-a035-aec120870bc1')"
);
        }
        
        public override void Down()
        {
        }
    }
}
