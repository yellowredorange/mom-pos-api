using Microsoft.EntityFrameworkCore;
using MomPosApi.Models;

namespace MomPosApi.Data {
    public class MomPosContext : DbContext {
        public MomPosContext(DbContextOptions<MomPosContext> options) : base(options) { }

        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MenuConfiguration> MenuConfigurations { get; set; }
        public DbSet<MenuItemOption> MenuItemOptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Category>()
       .HasOne(c => c.MenuConfiguration)
       .WithMany(mc => mc.Categories)
       .HasForeignKey(c => c.MenuConfigurationId)
       .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MenuItem>()
                .HasOne(mi => mi.Category)
                .WithMany(c => c.MenuItems)
                .HasForeignKey(mi => mi.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MenuItemOption>()
                .HasOne(mio => mio.MenuItem)
                .WithMany(mi => mi.MenuItemOptions)
                .HasForeignKey(mio => mio.MenuItemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MenuItem>()
                .Property(mi => mi.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<MenuItemOption>()
                .Property(mio => mio.AdditionalPrice)
                .HasColumnType("decimal(18,2)");
        }
    }
}
