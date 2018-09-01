namespace goalblog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.New", "ImageMimeType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.New", "ImageMimeType");
        }
    }
}
