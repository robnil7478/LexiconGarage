namespace LexiconGarage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class afterSeed : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.VehicleTypes", "TypeInSwedish", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VehicleTypes", "TypeInSwedish", c => c.String());
        }
    }
}
