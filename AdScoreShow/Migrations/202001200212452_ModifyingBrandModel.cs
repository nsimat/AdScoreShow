namespace AdScoreShow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyingBrandModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Brands", "Name", c => c.String(maxLength: 25));
            DropColumn("dbo.Brands", "BrandName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Brands", "BrandName", c => c.String(maxLength: 25));
            DropColumn("dbo.Brands", "Name");
        }
    }
}
