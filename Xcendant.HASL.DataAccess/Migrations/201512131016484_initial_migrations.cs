namespace Xcendant.HASL.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial_migrations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Hospitals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AdressLine01 = c.String(),
                        AddressLine02 = c.String(),
                        City = c.String(),
                        District = c.String(),
                        Province = c.String(),
                        Telephone = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RegisteredUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        AddressLine01 = c.String(),
                        AddressLine02 = c.String(),
                        City = c.String(),
                        District = c.String(),
                        Province = c.String(),
                        Country = c.String(),
                        HomeNumber = c.String(),
                        WorkNumber = c.String(),
                        MobileNumber = c.String(),
                        Email = c.String(),
                        IdentificaionType = c.Int(nullable: false),
                        IdentificationNumber = c.String(),
                        Gender = c.Int(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RegisteredUsers");
            DropTable("dbo.Hospitals");
        }
    }
}
