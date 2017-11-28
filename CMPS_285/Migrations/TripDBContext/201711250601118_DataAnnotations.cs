namespace CMPS_285.Migrations.TripDBContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataAnnotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Trips", "time", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trips", "time", c => c.DateTime(nullable: false));
        }
    }
}
