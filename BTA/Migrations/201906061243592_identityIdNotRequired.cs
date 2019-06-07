namespace BTA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class identityIdNotRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Traveler", "identityId", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Traveler", "identityId", c => c.String(nullable: false, maxLength: 128));
        }
    }
}
