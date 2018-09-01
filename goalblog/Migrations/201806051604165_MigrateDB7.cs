namespace goalblog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB7 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ChWorlds", name: "grope_Id", newName: "GropeId");
            RenameIndex(table: "dbo.ChWorlds", name: "IX_grope_Id", newName: "IX_GropeId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ChWorlds", name: "IX_GropeId", newName: "IX_grope_Id");
            RenameColumn(table: "dbo.ChWorlds", name: "GropeId", newName: "grope_Id");
        }
    }
}
