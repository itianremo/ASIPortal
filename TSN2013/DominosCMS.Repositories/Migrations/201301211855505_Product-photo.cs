namespace DominosCMS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Productphoto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Photo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Photo");
        }
    }
}
