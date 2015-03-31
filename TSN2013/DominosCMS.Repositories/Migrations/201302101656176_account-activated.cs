namespace DominosCMS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class accountactivated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "Activated", c => c.Boolean());
            AlterColumn("dbo.Companies", "Country", c => c.String());
            AlterColumn("dbo.Companies", "Logo", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Companies", "Logo", c => c.String(nullable: false));
            AlterColumn("dbo.Companies", "Country", c => c.String(nullable: false));
            DropColumn("dbo.Accounts", "Activated");
        }
    }
}
