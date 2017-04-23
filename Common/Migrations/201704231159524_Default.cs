namespace Common.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Default : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        SocialDescription = c.String(),
                        Content = c.String(),
                        PublishedUtcDateTime = c.DateTime(),
                        LinkedPosts = c.String(),
                        RawContent = c.String(),
                        LastUpdateUtcDateTime = c.DateTime(nullable: false),
                        DeleteUtcDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        SurName = c.String(),
                        Password = c.String(),
                        Login = c.String(),
                        IsPartner = c.Boolean(nullable: false),
                        About = c.String(),
                        IsWithoutAccess = c.Boolean(),
                        Rating = c.Int(nullable: false),
                        CreatedUtcDateTime = c.DateTime(nullable: false),
                        DeleteUtcDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BlogPostUsers",
                c => new
                    {
                        BlogPost_Id = c.Int(nullable: false),
                        User_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.BlogPost_Id, t.User_Id })
                .ForeignKey("dbo.BlogPosts", t => t.BlogPost_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.BlogPost_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BlogPostUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.BlogPostUsers", "BlogPost_Id", "dbo.BlogPosts");
            DropIndex("dbo.BlogPostUsers", new[] { "User_Id" });
            DropIndex("dbo.BlogPostUsers", new[] { "BlogPost_Id" });
            DropTable("dbo.BlogPostUsers");
            DropTable("dbo.Users");
            DropTable("dbo.BlogPosts");
        }
    }
}
