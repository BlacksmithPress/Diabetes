using System.Data.Entity.Migrations;

namespace BlacksmithPress.Diabetes.Persistence.Database.Migrations
{
    public partial class Measurements : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Measurements",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Attribute = c.Int(nullable: false),
                        UnitOfMeasure = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Subject_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Subject_Id)
                .Index(t => t.Subject_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Measurements", "Subject_Id", "dbo.People");
            DropIndex("dbo.Measurements", new[] { "Subject_Id" });
            DropTable("dbo.Measurements");
        }
    }
}
