namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "youtubeLink", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "youtubeLink", c => c.Int(nullable: false));
        }
    }
}
