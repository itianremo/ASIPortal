namespace DominosCMS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LegLength : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sections", "LegLength", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sections", "LegLength");
        }
    }
}
