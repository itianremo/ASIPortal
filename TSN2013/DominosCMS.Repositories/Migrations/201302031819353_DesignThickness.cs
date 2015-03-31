namespace DominosCMS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DesignThickness : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sections", "DesignThickness", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sections", "DesignThickness");
        }
    }
}
