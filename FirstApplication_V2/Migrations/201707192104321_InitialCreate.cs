namespace FirstApplication_V2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GameGenres",
                c => new
                    {
                        GameGenreId = c.String(nullable: false, maxLength: 128, defaultValueSql: "newid()"), 
                        GameId = c.String(nullable: false, maxLength: 128),
                        GenreId = c.String(nullable: false, maxLength: 128),
                        CreateDate = c.DateTime(nullable: false, defaultValueSql: "getutcdate()"),
                        EditDate = c.DateTime(nullable: false, defaultValueSql: "getutcdate()"),
                    })
                .PrimaryKey(t => t.GameGenreId)
                .ForeignKey("dbo.Games", t => t.GameId)
                .ForeignKey("dbo.Genres", t => t.GenreId)
                .Index(t => t.GameId)
                .Index(t => t.GenreId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        GameId = c.String(nullable: false, maxLength: 128, defaultValueSql: "newid()"),
                        Name = c.String(nullable: false, maxLength: 250),
                        IsMultiplayer = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false, defaultValueSql: "getutcdate()"),
                        EditDate = c.DateTime(nullable: false, defaultValueSql: "getutcdate()"),
                    })
                .PrimaryKey(t => t.GameId);
            
            //CreateTable(
            //    "dbo.Ratings",
            //    c => new
            //        {
            //            RatingId = c.String(nullable: false, maxLength: 128, defaultValueSql: "newid()"),
            //            UserId = c.String(nullable: false, maxLength: 128),
            //            GameId = c.String(nullable: false, maxLength: 128),
            //            Rank = c.Decimal(nullable: false, precision: 18, scale: 0),
            //            CreateDate = c.DateTime(nullable: false, defaultValueSql: "getutcdate()"),
            //            EditDate = c.DateTime(nullable: false, defaultValueSql: "getutcdate()"),
            //        })
            //    .PrimaryKey(t => t.RatingId)
            //    .ForeignKey("dbo.AspNetUsers", t => t.UserId)
            //    .ForeignKey("dbo.Games", t => t.GameId)
            //    .Index(t => t.UserId)
            //    .Index(t => t.GameId);
            
            //CreateTable(
            //    "dbo.ApplicationUsers",
            //    c => new
            //        {
            //            Id = c.String(nullable: false, maxLength: 128),
            //            Email = c.String(),
            //            EmailConfirmed = c.Boolean(nullable: false),
            //            PasswordHash = c.String(),
            //            SecurityStamp = c.String(),
            //            PhoneNumber = c.String(),
            //            PhoneNumberConfirmed = c.Boolean(nullable: false),
            //            TwoFactorEnabled = c.Boolean(nullable: false),
            //            LockoutEndDateUtc = c.DateTime(),
            //            LockoutEnabled = c.Boolean(nullable: false),
            //            AccessFailedCount = c.Int(nullable: false),
            //            UserName = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        GenreId = c.String(nullable: false, maxLength: 128, defaultValueSql: "newid()"),
                        Name = c.String(nullable: false, maxLength: 250),
                        CreateDate = c.DateTime(nullable: false, defaultValueSql: "getutcdate()"),
                        EditDate = c.DateTime(nullable: false, defaultValueSql: "getutcdate()"),
                    })
                .PrimaryKey(t => t.GenreId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GameGenres", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.Ratings", "GameId", "dbo.Games");
            //DropForeignKey("dbo.Ratings", "UserId", "dbo.ApplicationUsers");
            DropForeignKey("dbo.GameGenres", "GameId", "dbo.Games");
            //DropIndex("dbo.Ratings", new[] { "GameId" });
            //DropIndex("dbo.Ratings", new[] { "UserId" });
            DropIndex("dbo.GameGenres", new[] { "GenreId" });
            DropIndex("dbo.GameGenres", new[] { "GameId" });
            DropTable("dbo.Genres");
            DropTable("dbo.ApplicationUsers");
            //DropTable("dbo.Ratings");
            DropTable("dbo.Games");
            DropTable("dbo.GameGenres");
        }
    }
}
