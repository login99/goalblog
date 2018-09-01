namespace goalblog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TableFs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Icon = c.Binary(),
                        Name = c.String(),
                        Game = c.Int(nullable: false),
                        WinGame = c.Int(nullable: false),
                        DrawGame = c.Int(nullable: false),
                        LoseGame = c.Int(nullable: false),
                        Goal = c.Int(nullable: false),
                        MissGoal = c.Int(nullable: false),
                        Score = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TableFClasses",
                c => new
                    {
                        TableF_Id = c.Int(nullable: false),
                        Class_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TableF_Id, t.Class_Id })
                .ForeignKey("dbo.TableFs", t => t.TableF_Id, cascadeDelete: true)
                .ForeignKey("dbo.Class", t => t.Class_Id, cascadeDelete: true)
                .Index(t => t.TableF_Id)
                .Index(t => t.Class_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TableFClasses", "Class_Id", "dbo.Class");
            DropForeignKey("dbo.TableFClasses", "TableF_Id", "dbo.TableFs");
            DropIndex("dbo.TableFClasses", new[] { "Class_Id" });
            DropIndex("dbo.TableFClasses", new[] { "TableF_Id" });
            DropTable("dbo.TableFClasses");
            DropTable("dbo.TableFs");
        }
    }
}
