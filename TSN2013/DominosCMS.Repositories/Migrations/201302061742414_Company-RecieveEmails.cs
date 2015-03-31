namespace DominosCMS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompanyRecieveEmails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "RecieveEmails", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Companies", "Phone", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Companies", "Phone", c => c.String());
            DropColumn("dbo.Companies", "RecieveEmails");
        }
    }
}
