namespace BTA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cityCountryNotRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.City", "country", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.City", "country", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
