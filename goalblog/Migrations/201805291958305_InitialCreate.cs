namespace goalblog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Class",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.New",
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
                "dbo.NewClass",
                c => new
                    {
                        NewId = c.Int(nullable: false),
                        ClassId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.NewId, t.ClassId })
                .ForeignKey("dbo.New", t => t.NewId, cascadeDelete: true)
                .ForeignKey("dbo.Class", t => t.ClassId, cascadeDelete: true)
                .Index(t => t.NewId)
                .Index(t => t.ClassId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NewClass", "ClassId", "dbo.Class");
            DropForeignKey("dbo.NewClass", "NewId", "dbo.New");
            DropIndex("dbo.NewClass", new[] { "ClassId" });
            DropIndex("dbo.NewClass", new[] { "NewId" });
            DropTable("dbo.NewClass");
            DropTable("dbo.New");
            DropTable("dbo.Class");
        }
    }
}
