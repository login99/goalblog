namespace goalblog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.New", "ImageMimeType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.New", "ImageMimeType", c => c.String());
        }
    }
}
