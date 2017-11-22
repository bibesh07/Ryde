namespace CMPS_285.Migrations.TripDBContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trips",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        origin = c.String(),
                        destination = c.String(),
                        passanger1 = c.String(),
                        passanger2 = c.String(),
                        passanger3 = c.String(),
                        passanger4 = c.String(),
                        passanger5 = c.String(),
                        time = c.String(),
                        driver = c.String(),
                        seats = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Trips");
        }
    }
}
