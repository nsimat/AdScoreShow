namespace AdScoreShow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdvertAireds",
                c => new
                    {
                        AdvertisementID = c.Int(nullable: false),
                        MarketID = c.Int(nullable: false),
                        Year = c.Int(),
                        Score_1 = c.Int(),
                        Score_2 = c.Int(),
                    })
                .PrimaryKey(t => new { t.AdvertisementID, t.MarketID })
                .ForeignKey("dbo.Advertisements", t => t.AdvertisementID, cascadeDelete: true)
                .ForeignKey("dbo.Markets", t => t.MarketID, cascadeDelete: true)
                .Index(t => t.AdvertisementID)
                .Index(t => t.MarketID);
            
            CreateTable(
                "dbo.Advertisements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Copy_Name = c.String(),
                        Copy_Duration = c.String(),
                        SegmentID = c.Int(nullable: false),
                        BrandID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Segments", t => t.SegmentID, cascadeDelete: true)
                .ForeignKey("dbo.Brands", t => t.BrandID, cascadeDelete: true)
                .Index(t => new { t.Copy_Name, t.Copy_Duration }, unique: true, name: "SameAdvert")
                .Index(t => t.SegmentID)
                .Index(t => t.BrandID);
            
            CreateTable(
                "dbo.Segments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Category = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BrandName = c.String(),
                        SegmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Segments", t => t.SegmentID, cascadeDelete: true)
                .Index(t => t.SegmentID);
            
            CreateTable(
                "dbo.Markets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AdvertAireds", "MarketID", "dbo.Markets");
            DropForeignKey("dbo.Brands", "SegmentID", "dbo.Segments");
            DropForeignKey("dbo.Advertisements", "BrandID", "dbo.Brands");
            DropForeignKey("dbo.Advertisements", "SegmentID", "dbo.Segments");
            DropForeignKey("dbo.AdvertAireds", "AdvertisementID", "dbo.Advertisements");
            DropIndex("dbo.Brands", new[] { "SegmentID" });
            DropIndex("dbo.Advertisements", new[] { "BrandID" });
            DropIndex("dbo.Advertisements", new[] { "SegmentID" });
            DropIndex("dbo.Advertisements", "SameAdvert");
            DropIndex("dbo.AdvertAireds", new[] { "MarketID" });
            DropIndex("dbo.AdvertAireds", new[] { "AdvertisementID" });
            DropTable("dbo.Markets");
            DropTable("dbo.Brands");
            DropTable("dbo.Segments");
            DropTable("dbo.Advertisements");
            DropTable("dbo.AdvertAireds");
        }
    }
}
