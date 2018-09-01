namespace goalblog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChWorlds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumbGr = c.Int(nullable: false),
                        Icon = c.Binary(),
                        Name = c.String(),
                        Game = c.Int(nullable: false),
                        WinGame = c.Int(nullable: false),
                        DrawGame = c.Int(nullable: false),
                        LoseGame = c.Int(nullable: false),
                        Goal = c.Int(nullable: false),
                        MissGoal = c.Int(nullable: false),
                        Score = c.Int(nullable: false),
                        grope_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gropes", t => t.grope_Id)
                .Index(t => t.grope_Id);
            
            CreateTable(
                "dbo.Gropes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChWorlds", "grope_Id", "dbo.Gropes");
            DropIndex("dbo.ChWorlds", new[] { "grope_Id" });
            DropTable("dbo.Gropes");
            DropTable("dbo.ChWorlds");
        }
    }
}
