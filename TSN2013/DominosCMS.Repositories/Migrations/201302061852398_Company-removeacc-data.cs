namespace DominosCMS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Companyremoveaccdata : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Companies", "Email");
            DropColumn("dbo.Companies", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Companies", "Password", c => c.String());
            AddColumn("dbo.Companies", "Email", c => c.String());
        }
    }
}
