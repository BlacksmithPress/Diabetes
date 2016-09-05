namespace BlacksmithPress.Diabetes.Persistence.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUsernameToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "Username", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "Username");
        }
    }
}
