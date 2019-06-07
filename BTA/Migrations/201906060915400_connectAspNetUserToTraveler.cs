namespace BTA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class connectAspNetUserToTraveler : DbMigration
    {
        public override void Up()
        {
            Sql("ALTER TABLE Traveler ADD FOREIGN KEY(identityId) REFERENCES AspNetUsers(Id);");
        }
        
        public override void Down()
        {
        }
    }
}
