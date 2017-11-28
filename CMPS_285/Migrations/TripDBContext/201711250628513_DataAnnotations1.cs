namespace CMPS_285.Migrations.TripDBContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataAnnotations1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Trips", "origin", c => c.String(nullable: false));
            AlterColumn("dbo.Trips", "destination", c => c.String(nullable: false));
            AlterColumn("dbo.Trips", "time", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trips", "time", c => c.String());
            AlterColumn("dbo.Trips", "destination", c => c.String());
            AlterColumn("dbo.Trips", "origin", c => c.String());
        }
    }
}
