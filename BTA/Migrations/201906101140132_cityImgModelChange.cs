namespace BTA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cityImgModelChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.City", "imgUrl", c => c.String(maxLength: 2083));
            DropColumn("dbo.City", "image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.City", "image", c => c.String(maxLength: 2083));
            DropColumn("dbo.City", "imgUrl");
        }
    }
}
