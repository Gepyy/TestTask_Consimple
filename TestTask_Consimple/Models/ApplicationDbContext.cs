using Microsoft.EntityFrameworkCore;

namespace TestTask_Consimple.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseItem> PurchaseItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().ToTable("Client").HasKey(c => c.IDClient);
            modelBuilder.Entity<Product>().ToTable("Product").HasKey(p => p.IDProduct);
            modelBuilder.Entity<Purchase>().ToTable("Purchase").HasKey(p => p.Number);
            modelBuilder.Entity<PurchaseItem>().ToTable("PurchaseItem").HasKey(pi => pi.ID);

            modelBuilder.Entity<Purchase>()
                .HasOne(p => p.Client)
                .WithMany(c => c.Purchases)
                .HasForeignKey(p => p.IDClient);

            modelBuilder.Entity<PurchaseItem>()
                .HasOne(pi => pi.Purchase)
                .WithMany(p => p.PurchaseItems)
                .HasForeignKey(pi => pi.PurchaseNumber);

            modelBuilder.Entity<PurchaseItem>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.PurchaseItems)
                .HasForeignKey(pi => pi.IDProduct);
        }
    }
} 