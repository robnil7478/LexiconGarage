namespace LexiconGarage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class colorowner : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicles", "Owner", c => c.String(nullable: false));
            DropColumn("dbo.Vehicles", "Color");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vehicles", "Color", c => c.Int(nullable: false));
            DropColumn("dbo.Vehicles", "Owner");
        }
    }
}
