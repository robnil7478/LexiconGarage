namespace LexiconGarage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ettnamn : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Vehicles", "RegNo", c => c.String(nullable: false, maxLength: 6));
            AlterColumn("dbo.Vehicles", "Brand", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Vehicles", "Model", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Vehicles", "Model", c => c.String());
            AlterColumn("dbo.Vehicles", "Brand", c => c.String());
            AlterColumn("dbo.Vehicles", "RegNo", c => c.String());
        }
    }
}
