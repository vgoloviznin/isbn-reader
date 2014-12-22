namespace Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Addedbooks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Isbn = c.Long(nullable: false),
                        IsRead = c.Boolean(nullable: false, defaultValue: false),
                    })
                .PrimaryKey(t => t.Isbn);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Books");
        }
    }
}
