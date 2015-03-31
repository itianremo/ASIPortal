namespace DominosCMS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sectionupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sections", "Type", c => c.String());
            AddColumn("dbo.Sections", "WebDepth", c => c.String());
            AddColumn("dbo.Sections", "FlangeWidth", c => c.String());
            AddColumn("dbo.Sections", "MilThickness", c => c.String());
            AddColumn("dbo.Sections", "YeildStrength", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sections", "YeildStrength");
            DropColumn("dbo.Sections", "MilThickness");
            DropColumn("dbo.Sections", "FlangeWidth");
            DropColumn("dbo.Sections", "WebDepth");
            DropColumn("dbo.Sections", "Type");
        }
    }
}
