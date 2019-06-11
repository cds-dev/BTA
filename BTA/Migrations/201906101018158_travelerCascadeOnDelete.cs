namespace BTA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class travelerCascadeOnDelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Traveler", "identityId", "dbo.AspNetUsers");
            AddForeignKey("dbo.Traveler", "identityId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }

        public override void Down()
        {
            
        }
    }
}
