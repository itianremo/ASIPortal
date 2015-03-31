namespace DominosCMS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompanyType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "Type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Companies", "Type");
        }
    }
}
