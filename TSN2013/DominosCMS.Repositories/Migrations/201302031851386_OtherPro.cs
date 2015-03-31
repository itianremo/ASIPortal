namespace DominosCMS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OtherPro : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sections", "Width", c => c.String());
            AddColumn("dbo.Sections", "MinSteelThickness", c => c.String());
            AddColumn("dbo.Sections", "MPa", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sections", "MPa");
            DropColumn("dbo.Sections", "MinSteelThickness");
            DropColumn("dbo.Sections", "Width");
        }
    }
}
