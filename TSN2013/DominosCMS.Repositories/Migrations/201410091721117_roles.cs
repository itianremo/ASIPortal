namespace DominosCMS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class roles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MenuItemGroups",
                c => new
                    {
                        MenuItem_ID = c.Int(nullable: false),
                        Group_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MenuItem_ID, t.Group_ID })
                .ForeignKey("dbo.MenuItems", t => t.MenuItem_ID, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.Group_ID, cascadeDelete: true)
                .Index(t => t.MenuItem_ID)
                .Index(t => t.Group_ID);
            
            CreateTable(
                "dbo.MenuItemAccounts",
                c => new
                    {
                        MenuItem_ID = c.Int(nullable: false),
                        Account_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MenuItem_ID, t.Account_ID })
                .ForeignKey("dbo.MenuItems", t => t.MenuItem_ID, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.Account_ID, cascadeDelete: true)
                .Index(t => t.MenuItem_ID)
                .Index(t => t.Account_ID);
            
            CreateTable(
                "dbo.PageGroups",
                c => new
                    {
                        Page_ID = c.Int(nullable: false),
                        Group_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Page_ID, t.Group_ID })
                .ForeignKey("dbo.Pages", t => t.Page_ID, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.Group_ID, cascadeDelete: true)
                .Index(t => t.Page_ID)
                .Index(t => t.Group_ID);
            
            CreateTable(
                "dbo.PageAccounts",
                c => new
                    {
                        Page_ID = c.Int(nullable: false),
                        Account_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Page_ID, t.Account_ID })
                .ForeignKey("dbo.Pages", t => t.Page_ID, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.Account_ID, cascadeDelete: true)
                .Index(t => t.Page_ID)
                .Index(t => t.Account_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PageAccounts", "Account_ID", "dbo.Accounts");
            DropForeignKey("dbo.PageAccounts", "Page_ID", "dbo.Pages");
            DropForeignKey("dbo.PageGroups", "Group_ID", "dbo.Groups");
            DropForeignKey("dbo.PageGroups", "Page_ID", "dbo.Pages");
            DropForeignKey("dbo.MenuItemAccounts", "Account_ID", "dbo.Accounts");
            DropForeignKey("dbo.MenuItemAccounts", "MenuItem_ID", "dbo.MenuItems");
            DropForeignKey("dbo.MenuItemGroups", "Group_ID", "dbo.Groups");
            DropForeignKey("dbo.MenuItemGroups", "MenuItem_ID", "dbo.MenuItems");
            DropIndex("dbo.PageAccounts", new[] { "Account_ID" });
            DropIndex("dbo.PageAccounts", new[] { "Page_ID" });
            DropIndex("dbo.PageGroups", new[] { "Group_ID" });
            DropIndex("dbo.PageGroups", new[] { "Page_ID" });
            DropIndex("dbo.MenuItemAccounts", new[] { "Account_ID" });
            DropIndex("dbo.MenuItemAccounts", new[] { "MenuItem_ID" });
            DropIndex("dbo.MenuItemGroups", new[] { "Group_ID" });
            DropIndex("dbo.MenuItemGroups", new[] { "MenuItem_ID" });
            DropColumn("dbo.MenuItems", "IsPublic");
            DropTable("dbo.PageAccounts");
            DropTable("dbo.PageGroups");
            DropTable("dbo.MenuItemAccounts");
            DropTable("dbo.MenuItemGroups");
        }
    }
}
