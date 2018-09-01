using goalblog.Models;
using System.Data.Entity;

namespace WebApplication1.Models
{
    public class NewsContext:DbContext
    {
        public DbSet<New> News { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<TableF> Table { get; set; }
        public DbSet<Grope> gropes { get; set; }
        public DbSet<ChWorld> chWorlds { get; set; }
        public DbSet<TrNews> TrNewses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<New>().HasMany(c => c.Class)
                .WithMany(s => s.News)
                .Map(t => t.MapLeftKey("NewId")
                .MapRightKey("ClassId")
                .ToTable("NewClass"));
            modelBuilder.Entity<TrNews>().HasMany(c => c.Class)
                .WithMany(s => s.TrNewses)
                .Map(t => t.MapLeftKey("TrNewId")
                .MapRightKey("ClassId")
                .ToTable("TrNewClass"));
            modelBuilder.Entity<TableF>().HasMany(c => c.Classes)
               .WithMany(s => s.Tableses)
               .Map(t => t.MapLeftKey("TebleId")
               .MapRightKey("ClassId")
               .ToTable("TebleClass"));
        }
    }
}