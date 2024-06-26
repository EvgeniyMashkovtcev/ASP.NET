using Microsoft.EntityFrameworkCore;
using WebAppGB.Models;

namespace WebAppGB.Data
{
    public class StorageContext : DbContext 
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductGroup> ProductGroup { get; set; }
        public virtual DbSet<Storage> Storage { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("Data Source =.\\SQLEXPRESS")
            .UseLazyLoadingProxies()
            .LogTo(Console.WriteLine);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductGroup>(entity =>
            {
                entity.HasKey(pg => pg.Id)
                      .HasName("product_group_pk");

                entity.ToTable("category");

                entity.Property(pg => pg.Name)
                      .HasColumnName("name")
                      .HasMaxLength(225);
            });
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(pg => pg.Id)
                      .HasName("product_pk");

                entity.Property(pg => pg.Name)
                      .HasColumnName("name")
                      .HasMaxLength(225);
                entity.HasOne(p => p.ProductGroup).WithMany(p => p.Products).HasForeignKey(p => p.ProductGroupId);
            });
            modelBuilder.Entity<Storage>(entity =>
            {
                entity.HasKey(pg => pg.Id)
                      .HasName("storage_pk");

                entity.HasOne(s => s.Product).WithMany(p => p.Storages).HasForeignKey(p => p.ProductId);
            });

        }


    }
}
