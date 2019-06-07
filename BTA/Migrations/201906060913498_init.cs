namespace BTA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Category",
            //    c => new
            //        {
            //            categoryId = c.Long(nullable: false, identity: true),
            //            category = c.String(nullable: false, maxLength: 50),
            //            module = c.Long(),
            //        })
            //    .PrimaryKey(t => t.categoryId)
            //    .ForeignKey("dbo.Module", t => t.module)
            //    .Index(t => t.module);
            
            //CreateTable(
            //    "dbo.Comment",
            //    c => new
            //        {
            //            commentId = c.Long(nullable: false),
            //            parentId = c.Long(),
            //            traveler = c.Long(),
            //            poi = c.String(maxLength: 256),
            //            category = c.Long(),
            //            subject = c.String(maxLength: 50),
            //            date = c.DateTime(nullable: false, storeType: "date"),
            //            text = c.String(maxLength: 500),
            //            grade = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.commentId)
            //    .ForeignKey("dbo.Comment", t => t.parentId)
            //    .ForeignKey("dbo.POI", t => t.poi)
            //    .ForeignKey("dbo.Traveler", t => t.traveler)
            //    .ForeignKey("dbo.Category", t => t.category)
            //    .Index(t => t.parentId)
            //    .Index(t => t.traveler)
            //    .Index(t => t.poi)
            //    .Index(t => t.category);
            
            //CreateTable(
            //    "dbo.POI",
            //    c => new
            //        {
            //            poiId = c.String(nullable: false, maxLength: 256),
            //            city = c.String(maxLength: 256),
            //            name = c.String(nullable: false, maxLength: 50),
            //            address = c.String(maxLength: 150),
            //            website = c.String(maxLength: 2083),
            //            poiImg = c.String(maxLength: 2083),
            //            rating = c.Double(nullable: false),
            //            lon = c.Double(),
            //            lat = c.Double(),
            //            phone = c.String(maxLength: 50),
            //            email = c.String(maxLength: 50),
            //            category = c.Long(),
            //        })
            //    .PrimaryKey(t => t.poiId)
            //    .ForeignKey("dbo.City", t => t.city)
            //    .ForeignKey("dbo.Category", t => t.category)
            //    .Index(t => t.city)
            //    .Index(t => t.category);
            
            //CreateTable(
            //    "dbo.City",
            //    c => new
            //        {
            //            cityId = c.String(nullable: false, maxLength: 256),
            //            city = c.String(nullable: false, maxLength: 20),
            //            country = c.String(nullable: false, maxLength: 20),
            //            lon = c.Double(),
            //            lat = c.Double(),
            //            image = c.String(maxLength: 2083),
            //            population = c.Int(),
            //        })
            //    .PrimaryKey(t => t.cityId);
            
            //CreateTable(
            //    "dbo.Traveler",
            //    c => new
            //        {
            //            travelerId = c.Long(nullable: false, identity: true),
            //            identityId = c.String(nullable: false, maxLength: 128),
            //            activity = c.Boolean(nullable: false),
            //            imgUrl = c.String(maxLength: 150),
            //            fullName = c.String(maxLength: 10, fixedLength: true),
            //        })
            //    .PrimaryKey(t => t.travelerId);
            
            //CreateTable(
            //    "dbo.Module",
            //    c => new
            //        {
            //            moduleId = c.Long(nullable: false, identity: true),
            //            module = c.String(nullable: false, maxLength: 50),
            //        })
            //    .PrimaryKey(t => t.moduleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            //CreateTable(
            //    "dbo.sysdiagrams",
            //    c => new
            //        {
            //            diagram_id = c.Int(nullable: false, identity: true),
            //            name = c.String(nullable: false, maxLength: 128),
            //            principal_id = c.Int(nullable: false),
            //            version = c.Int(),
            //            definition = c.Binary(),
            //        })
            //    .PrimaryKey(t => t.diagram_id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.POI", "category", "dbo.Category");
            DropForeignKey("dbo.Category", "module", "dbo.Module");
            DropForeignKey("dbo.Comment", "category", "dbo.Category");
            DropForeignKey("dbo.Comment", "traveler", "dbo.Traveler");
            DropForeignKey("dbo.Comment", "poi", "dbo.POI");
            DropForeignKey("dbo.POI", "city", "dbo.City");
            DropForeignKey("dbo.Comment", "parentId", "dbo.Comment");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.POI", new[] { "category" });
            DropIndex("dbo.POI", new[] { "city" });
            DropIndex("dbo.Comment", new[] { "category" });
            DropIndex("dbo.Comment", new[] { "poi" });
            DropIndex("dbo.Comment", new[] { "traveler" });
            DropIndex("dbo.Comment", new[] { "parentId" });
            DropIndex("dbo.Category", new[] { "module" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Module");
            DropTable("dbo.Traveler");
            DropTable("dbo.City");
            DropTable("dbo.POI");
            DropTable("dbo.Comment");
            DropTable("dbo.Category");
        }
    }
}
