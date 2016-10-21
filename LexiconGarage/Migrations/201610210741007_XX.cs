namespace LexiconGarage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class XX : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicles", "ParkingSlot", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vehicles", "ParkingSlot");
        }
    }
}
