namespace DominosCMS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TechPropViewOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TechnicalProperties", "ViewOrder", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "Title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Title", c => c.String());
            DropColumn("dbo.TechnicalProperties", "ViewOrder");
        }
    }
}
