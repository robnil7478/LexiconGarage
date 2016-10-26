namespace LexiconGarage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmember : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        TelNumber = c.String(nullable: false),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VehicleTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeInSwedish = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Vehicles", "VehicleTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Vehicles", "MemberId", c => c.Int(nullable: false));
            CreateIndex("dbo.Vehicles", "VehicleTypeId");
            CreateIndex("dbo.Vehicles", "MemberId");
            AddForeignKey("dbo.Vehicles", "MemberId", "dbo.Members", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Vehicles", "VehicleTypeId", "dbo.VehicleTypes", "Id", cascadeDelete: false);
            DropColumn("dbo.Vehicles", "Type");
            DropColumn("dbo.Vehicles", "Owner");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vehicles", "Owner", c => c.String(nullable: false));
            AddColumn("dbo.Vehicles", "Type", c => c.Int(nullable: false));
            DropForeignKey("dbo.Vehicles", "VehicleTypeId", "dbo.VehicleTypes");
            DropForeignKey("dbo.Vehicles", "MemberId", "dbo.Members");
            DropIndex("dbo.Vehicles", new[] { "MemberId" });
            DropIndex("dbo.Vehicles", new[] { "VehicleTypeId" });
            DropColumn("dbo.Vehicles", "MemberId");
            DropColumn("dbo.Vehicles", "VehicleTypeId");
            DropTable("dbo.VehicleTypes");
            DropTable("dbo.Members");
        }
    }
}
