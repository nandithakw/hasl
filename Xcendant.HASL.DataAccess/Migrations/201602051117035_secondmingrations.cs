namespace Xcendant.HASL.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secondmingrations : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Patients", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Patients", "Name", c => c.String());
        }
    }
}
