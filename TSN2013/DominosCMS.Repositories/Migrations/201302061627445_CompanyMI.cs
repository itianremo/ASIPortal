namespace DominosCMS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompanyMI : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "MiddleInitial", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Companies", "MiddleInitial");
        }
    }
}
