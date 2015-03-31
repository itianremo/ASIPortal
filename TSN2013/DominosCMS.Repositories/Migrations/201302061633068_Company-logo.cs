namespace DominosCMS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Companylogo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "Logo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Companies", "Logo");
        }
    }
}
