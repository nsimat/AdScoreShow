namespace AdScoreShow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyingAdvertisementModel : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Advertisements", "SameAdvert");
            AlterColumn("dbo.Advertisements", "Copy_Duration", c => c.Int(nullable: false));
            CreateIndex("dbo.Advertisements", new[] { "Copy_Name", "Copy_Duration" }, unique: true, name: "SameAdvert");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Advertisements", "SameAdvert");
            AlterColumn("dbo.Advertisements", "Copy_Duration", c => c.String(maxLength: 25));
            CreateIndex("dbo.Advertisements", new[] { "Copy_Name", "Copy_Duration" }, unique: true, name: "SameAdvert");
        }
    }
}
