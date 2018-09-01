namespace goalblog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TableFs", "NumberTable", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TableFs", "NumberTable");
        }
    }
}
