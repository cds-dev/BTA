namespace BTA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class travelerFullNameChanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Traveler", "fullName", c => c.String(maxLength: 50, fixedLength: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Traveler", "fullName", c => c.String(maxLength: 10, fixedLength: true));
        }
    }
}
