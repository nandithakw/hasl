namespace Xcendant.HASL.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CareGivers",
                c => new
                    {
                        RegisteredUserId = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.RegisteredUserId)
                .ForeignKey("dbo.RegisteredUsers", t => t.RegisteredUserId)
                .Index(t => t.RegisteredUserId);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        RegisteredUserId = c.Int(nullable: false),
                        Name = c.String(),
                        RegistrationNumber = c.String(),
                        HemophiliaType = c.Int(nullable: false),
                        HemophiliaServity = c.Int(nullable: false),
                        CounsultingDoctorId = c.Int(nullable: false),
                        CounsultingHospitalId = c.Int(nullable: false),
                        PersonelCareGiverId = c.Int(nullable: false),
                        ConusltingTreatmentCenterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RegisteredUserId)
                .ForeignKey("dbo.TreatmentCenters", t => t.ConusltingTreatmentCenterId)
                .ForeignKey("dbo.Hospitals", t => t.CounsultingHospitalId)
                .ForeignKey("dbo.Doctors", t => t.CounsultingDoctorId)
                .ForeignKey("dbo.CareGivers", t => t.PersonelCareGiverId)
                .ForeignKey("dbo.RegisteredUsers", t => t.RegisteredUserId)
                .Index(t => t.RegisteredUserId)
                .Index(t => t.CounsultingDoctorId)
                .Index(t => t.CounsultingHospitalId)
                .Index(t => t.PersonelCareGiverId)
                .Index(t => t.ConusltingTreatmentCenterId);
            
            CreateTable(
                "dbo.TreatmentCenters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Telephone = c.String(nullable: false),
                        Mobile = c.String(),
                        Email = c.String(nullable: false),
                        AddressLine01 = c.String(nullable: false),
                        AddressLine02 = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Province = c.String(nullable: false),
                        District = c.String(nullable: false),
                        Country = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        RegisteredUserId = c.Int(nullable: false),
                        RegistraionNumber = c.String(nullable: false),
                        HospitalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RegisteredUserId)
                .ForeignKey("dbo.Hospitals", t => t.HospitalId)
                .ForeignKey("dbo.RegisteredUsers", t => t.RegisteredUserId)
                .Index(t => t.RegisteredUserId)
                .Index(t => t.HospitalId);
            
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
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        AddressLine01 = c.String(nullable: false),
                        AddressLine02 = c.String(nullable: false),
                        City = c.String(nullable: false),
                        District = c.String(),
                        Province = c.String(),
                        Country = c.String(nullable: false),
                        HomeNumber = c.String(),
                        WorkNumber = c.String(),
                        MobileNumber = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        IdentificaionType = c.Int(nullable: false),
                        IdentificationNumber = c.String(nullable: false),
                        Gender = c.Int(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CareGivers", "RegisteredUserId", "dbo.RegisteredUsers");
            DropForeignKey("dbo.Patients", "RegisteredUserId", "dbo.RegisteredUsers");
            DropForeignKey("dbo.Patients", "PersonelCareGiverId", "dbo.CareGivers");
            DropForeignKey("dbo.Doctors", "RegisteredUserId", "dbo.RegisteredUsers");
            DropForeignKey("dbo.ProfileImages", "RegisteredUserId", "dbo.RegisteredUsers");
            DropForeignKey("dbo.Patients", "CounsultingDoctorId", "dbo.Doctors");
            DropForeignKey("dbo.Patients", "CounsultingHospitalId", "dbo.Hospitals");
            DropForeignKey("dbo.Doctors", "HospitalId", "dbo.Hospitals");
            DropForeignKey("dbo.Patients", "ConusltingTreatmentCenterId", "dbo.TreatmentCenters");
            DropIndex("dbo.ProfileImages", new[] { "RegisteredUserId" });
            DropIndex("dbo.Doctors", new[] { "HospitalId" });
            DropIndex("dbo.Doctors", new[] { "RegisteredUserId" });
            DropIndex("dbo.Patients", new[] { "ConusltingTreatmentCenterId" });
            DropIndex("dbo.Patients", new[] { "PersonelCareGiverId" });
            DropIndex("dbo.Patients", new[] { "CounsultingHospitalId" });
            DropIndex("dbo.Patients", new[] { "CounsultingDoctorId" });
            DropIndex("dbo.Patients", new[] { "RegisteredUserId" });
            DropIndex("dbo.CareGivers", new[] { "RegisteredUserId" });
            DropTable("dbo.ProfileImages");
            DropTable("dbo.RegisteredUsers");
            DropTable("dbo.Hospitals");
            DropTable("dbo.Doctors");
            DropTable("dbo.TreatmentCenters");
            DropTable("dbo.Patients");
            DropTable("dbo.CareGivers");
        }
    }
}
