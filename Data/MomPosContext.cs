using Microsoft.EntityFrameworkCore;
using MomPosApi.Models;

namespace MomPosApi.Data {
  public class MomPosContext : DbContext {
    public MomPosContext(DbContextOptions<MomPosContext> options) : base(options) { }

    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<MenuItemOption> MenuItemOptions { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CategoryMenuItem> CategoryMenuItems { get; set; }
    public DbSet<MenuConfiguration> MenuConfigurations { get; set; }
    public DbSet<MenuConfigurationCategory> MenuConfigurationCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      modelBuilder.Entity<MenuItem>()
          .HasMany(mi => mi.Options)
          .WithOne(mio => mio.MenuItem)
          .HasForeignKey(mio => mio.MenuItemId);

      modelBuilder.Entity<MenuItem>()
          .HasMany(mi => mi.CategoryMenuItems)
          .WithOne(cmi => cmi.MenuItem)
          .HasForeignKey(cmi => cmi.MenuItemId);

      modelBuilder.Entity<Category>()
          .HasMany(c => c.CategoryMenuItems)
          .WithOne(cmi => cmi.Category)
          .HasForeignKey(cmi => cmi.CategoryId);

      modelBuilder.Entity<MenuConfiguration>()
          .HasMany(mc => mc.MenuConfigurationCategories)
          .WithOne(mcc => mcc.MenuConfiguration)
          .HasForeignKey(mcc => mcc.MenuConfigurationId);

      modelBuilder.Entity<MenuConfigurationCategory>()
          .HasOne(mcc => mcc.Category)
          .WithMany(c => c.MenuConfigurationCategories)
          .HasForeignKey(mcc => mcc.CategoryId);

      modelBuilder.Entity<MenuItem>()
          .Property(mi => mi.Price)
          .HasColumnType("decimal(18,2)");
    }
  }
}
