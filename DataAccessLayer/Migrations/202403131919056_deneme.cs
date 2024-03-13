namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deneme : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "RePassword");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "RePassword", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
