namespace Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class addedmorebookfields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Title", c => c.String(nullable: false));
            AddColumn("dbo.Books", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "ImageUrl");
            DropColumn("dbo.Books", "Title");
        }
    }
}
