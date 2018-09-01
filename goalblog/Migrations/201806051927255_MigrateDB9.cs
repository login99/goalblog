namespace goalblog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB9 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TrNews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Author = c.String(),
                        date = c.DateTime(nullable: false),
                        Text = c.String(),
                        View = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TrNewClass",
                c => new
                    {
                        TrNewId = c.Int(nullable: false),
                        ClassId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TrNewId, t.ClassId })
                .ForeignKey("dbo.TrNews", t => t.TrNewId, cascadeDelete: true)
                .ForeignKey("dbo.Class", t => t.ClassId, cascadeDelete: true)
                .Index(t => t.TrNewId)
                .Index(t => t.ClassId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrNewClass", "ClassId", "dbo.Class");
            DropForeignKey("dbo.TrNewClass", "TrNewId", "dbo.TrNews");
            DropIndex("dbo.TrNewClass", new[] { "ClassId" });
            DropIndex("dbo.TrNewClass", new[] { "TrNewId" });
            DropTable("dbo.TrNewClass");
            DropTable("dbo.TrNews");
        }
    }
}
