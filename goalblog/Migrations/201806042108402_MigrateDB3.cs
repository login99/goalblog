namespace goalblog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB3 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TableFClasses", newName: "TebleClass");
            RenameColumn(table: "dbo.TebleClass", name: "TableF_Id", newName: "TebleId");
            RenameColumn(table: "dbo.TebleClass", name: "Class_Id", newName: "ClassId");
            RenameIndex(table: "dbo.TebleClass", name: "IX_TableF_Id", newName: "IX_TebleId");
            RenameIndex(table: "dbo.TebleClass", name: "IX_Class_Id", newName: "IX_ClassId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.TebleClass", name: "IX_ClassId", newName: "IX_Class_Id");
            RenameIndex(table: "dbo.TebleClass", name: "IX_TebleId", newName: "IX_TableF_Id");
            RenameColumn(table: "dbo.TebleClass", name: "ClassId", newName: "Class_Id");
            RenameColumn(table: "dbo.TebleClass", name: "TebleId", newName: "TableF_Id");
            RenameTable(name: "dbo.TebleClass", newName: "TableFClasses");
        }
    }
}
