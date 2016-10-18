namespace LexiconGarage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        RegNo = c.String(nullable: false, maxLength: 6),
                        Owner = c.String(nullable: false),
                        ParkingTime = c.DateTime(nullable: false),
                        NumberOfWheels = c.Int(nullable: false),
                        Brand = c.String(nullable: false, maxLength: 30),
                        Model = c.String(nullable: false, maxLength: 30),
                        Weight = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Vehicles");
        }
    }
}
