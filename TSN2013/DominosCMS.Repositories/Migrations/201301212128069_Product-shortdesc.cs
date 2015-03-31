namespace DominosCMS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Productshortdesc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ShortDesc", c => c.String());
            DropColumn("dbo.Products", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Name", c => c.String());
            DropColumn("dbo.Products", "ShortDesc");
        }
    }
}
