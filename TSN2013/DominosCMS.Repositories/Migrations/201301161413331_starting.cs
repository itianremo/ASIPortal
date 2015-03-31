namespace DominosCMS.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class starting : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Roles = c.String(),
                        Company_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Companies", t => t.Company_ID)
                .Index(t => t.Company_ID);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Address2 = c.String(),
                        City = c.String(nullable: false),
                        State = c.String(nullable: false),
                        ZipCode = c.String(nullable: false),
                        Country = c.String(nullable: false),
                        CompanyName = c.String(),
                        Phone = c.String(),
                        Fax = c.String(),
                        Email = c.String(),
                        WebSite = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Title = c.String(),
                        URL = c.String(),
                        Content = c.String(),
                        Keywords = c.String(),
                        Visible = c.Boolean(nullable: false),
                        Company_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Companies", t => t.Company_ID)
                .Index(t => t.Company_ID);
            
            CreateTable(
                "dbo.SpecificationItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Content = c.String(),
                        Filename = c.String(),
                        ViewOrder = c.Int(nullable: false),
                        Product_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.Product_ID)
                .Index(t => t.Product_ID);
            
            CreateTable(
                "dbo.ProductFiles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Type = c.String(),
                        Filename = c.String(),
                        Visible = c.Boolean(nullable: false),
                        Product_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.Product_ID)
                .Index(t => t.Product_ID);
            
            CreateTable(
                "dbo.TechnicalProperties",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Product_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.Product_ID)
                .Index(t => t.Product_ID);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Product_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.Product_ID)
                .Index(t => t.Product_ID);
            
            CreateTable(
                "dbo.PropertyValues",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        Property_ID = c.Int(),
                        Section_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TechnicalProperties", t => t.Property_ID)
                .ForeignKey("dbo.Sections", t => t.Section_ID)
                .Index(t => t.Property_ID)
                .Index(t => t.Section_ID);
            
            CreateTable(
                "dbo.Submittals",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Company_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Companies", t => t.Company_ID)
                .Index(t => t.Company_ID);
            
            CreateTable(
                "dbo.Pages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Url = c.String(nullable: false),
                        Contents = c.String(),
                        Keywords = c.String(),
                        Description = c.String(),
                        SplashPhoto = c.String(),
                        Visible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MenuItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Url = c.String(),
                        TypeCode = c.Byte(nullable: false),
                        Visible = c.Boolean(nullable: false),
                        Target = c.String(),
                        CssClass = c.String(),
                        ViewOrder = c.Int(nullable: false),
                        Parent_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MenuItems", t => t.Parent_ID)
                .Index(t => t.Parent_ID);
            
            CreateTable(
                "dbo.PhotoAlbums",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        ViewOrder = c.Int(nullable: false),
                        Visible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Filename = c.String(),
                        Visible = c.Boolean(nullable: false),
                        Album_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PhotoAlbums", t => t.Album_ID)
                .Index(t => t.Album_ID);
            
            CreateTable(
                "dbo.Banners",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Details = c.String(),
                        Photo = c.String(),
                        ShowInHome = c.Boolean(nullable: false),
                        ViewOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.NewsItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Briefe = c.String(),
                        Details = c.String(),
                        Photo = c.String(),
                        LastUpdate = c.DateTime(),
                        ShowInHome = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Inquiries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Telephone = c.String(nullable: false),
                        Message = c.String(nullable: false),
                        SubmissionDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Photos", new[] { "Album_ID" });
            DropIndex("dbo.MenuItems", new[] { "Parent_ID" });
            DropIndex("dbo.Submittals", new[] { "Company_ID" });
            DropIndex("dbo.PropertyValues", new[] { "Section_ID" });
            DropIndex("dbo.PropertyValues", new[] { "Property_ID" });
            DropIndex("dbo.Sections", new[] { "Product_ID" });
            DropIndex("dbo.TechnicalProperties", new[] { "Product_ID" });
            DropIndex("dbo.ProductFiles", new[] { "Product_ID" });
            DropIndex("dbo.SpecificationItems", new[] { "Product_ID" });
            DropIndex("dbo.Products", new[] { "Company_ID" });
            DropIndex("dbo.Accounts", new[] { "Company_ID" });
            DropForeignKey("dbo.Photos", "Album_ID", "dbo.PhotoAlbums");
            DropForeignKey("dbo.MenuItems", "Parent_ID", "dbo.MenuItems");
            DropForeignKey("dbo.Submittals", "Company_ID", "dbo.Companies");
            DropForeignKey("dbo.PropertyValues", "Section_ID", "dbo.Sections");
            DropForeignKey("dbo.PropertyValues", "Property_ID", "dbo.TechnicalProperties");
            DropForeignKey("dbo.Sections", "Product_ID", "dbo.Products");
            DropForeignKey("dbo.TechnicalProperties", "Product_ID", "dbo.Products");
            DropForeignKey("dbo.ProductFiles", "Product_ID", "dbo.Products");
            DropForeignKey("dbo.SpecificationItems", "Product_ID", "dbo.Products");
            DropForeignKey("dbo.Products", "Company_ID", "dbo.Companies");
            DropForeignKey("dbo.Accounts", "Company_ID", "dbo.Companies");
            DropTable("dbo.Inquiries");
            DropTable("dbo.NewsItems");
            DropTable("dbo.Banners");
            DropTable("dbo.Photos");
            DropTable("dbo.PhotoAlbums");
            DropTable("dbo.MenuItems");
            DropTable("dbo.Pages");
            DropTable("dbo.Submittals");
            DropTable("dbo.PropertyValues");
            DropTable("dbo.Sections");
            DropTable("dbo.TechnicalProperties");
            DropTable("dbo.ProductFiles");
            DropTable("dbo.SpecificationItems");
            DropTable("dbo.Products");
            DropTable("dbo.Companies");
            DropTable("dbo.Accounts");
        }
    }
}
