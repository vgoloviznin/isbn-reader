namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removedfields : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Books", "Title");
            DropColumn("dbo.Books", "ImageUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "ImageUrl", c => c.String());
            AddColumn("dbo.Books", "Title", c => c.String(nullable: false));
        }
    }
}
