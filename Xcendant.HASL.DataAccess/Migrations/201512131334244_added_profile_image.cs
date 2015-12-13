namespace Xcendant.HASL.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_profile_image : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProfileImages",
                c => new
                    {
                        RegisteredUserId = c.Int(nullable: false),
                        FileName = c.String(),
                        Description = c.String(),
                        ContentType = c.String(),
                        Content = c.Binary(),
                    })
                .PrimaryKey(t => t.RegisteredUserId)
                .ForeignKey("dbo.RegisteredUsers", t => t.RegisteredUserId)
                .Index(t => t.RegisteredUserId);
            
            AlterColumn("dbo.RegisteredUsers", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.RegisteredUsers", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.RegisteredUsers", "AddressLine01", c => c.String(nullable: false));
            AlterColumn("dbo.RegisteredUsers", "AddressLine02", c => c.String(nullable: false));
            AlterColumn("dbo.RegisteredUsers", "City", c => c.String(nullable: false));
            AlterColumn("dbo.RegisteredUsers", "Country", c => c.String(nullable: false));
            AlterColumn("dbo.RegisteredUsers", "MobileNumber", c => c.String(nullable: false));
            AlterColumn("dbo.RegisteredUsers", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.RegisteredUsers", "IdentificationNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProfileImages", "RegisteredUserId", "dbo.RegisteredUsers");
            DropIndex("dbo.ProfileImages", new[] { "RegisteredUserId" });
            AlterColumn("dbo.RegisteredUsers", "IdentificationNumber", c => c.String());
            AlterColumn("dbo.RegisteredUsers", "Email", c => c.String());
            AlterColumn("dbo.RegisteredUsers", "MobileNumber", c => c.String());
            AlterColumn("dbo.RegisteredUsers", "Country", c => c.String());
            AlterColumn("dbo.RegisteredUsers", "City", c => c.String());
            AlterColumn("dbo.RegisteredUsers", "AddressLine02", c => c.String());
            AlterColumn("dbo.RegisteredUsers", "AddressLine01", c => c.String());
            AlterColumn("dbo.RegisteredUsers", "LastName", c => c.String());
            AlterColumn("dbo.RegisteredUsers", "FirstName", c => c.String());
            DropTable("dbo.ProfileImages");
        }
    }
}
