using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using POS.Entity.Product;
using POS.Entity.Attendance;
using POS.Entity.Inovice;
using POS.Entity.Person;
using POS.Entity;

namespace POS.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<PurchaseInvoice> Purchases { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }

        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<SaleInvoice> Sales { get; set; }
        
        public DbSet<ProductBatch> Batches { get; set; }

        public DbSet<ImportInfo> ImportInfos { get; set; }
        
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<AttendanceDay> AttendanceDays { get; set; }
        public DbSet<AttendancePunch> AttendancePunches { get; set; }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Product entity
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ProductName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.ProductCode).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Barcode).HasMaxLength(100);
            });

            // Configure Sale entity
            modelBuilder.Entity<SaleInvoice>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.BuyerId).IsRequired();
                entity.HasOne(e => e.Buyer)
                      .WithMany(b => b.Sales)
                      .HasForeignKey(e => e.BuyerId);
            });

            // Configure Purchase entity
            modelBuilder.Entity<PurchaseInvoice>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Supplier)
                      .WithMany(s => s.Purchases)
                      .HasForeignKey(e => e.SupplierId);
            });

            // Configure Supplier entity
            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Person)
                      .WithOne(p => p.Supplier)
                      .HasForeignKey<Supplier>(s => s.PersonId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // Configure Batch entity
            modelBuilder.Entity<ProductBatch>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Product)
                      .WithMany(p => p.Batches)
                      .HasForeignKey(e => e.ProductId)
                      .OnDelete(DeleteBehavior.SetNull); // Allow null ProductId for empty batches
                //to do
                entity.Property(e => e.MRP).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.SaleRate).IsRequired().HasColumnType("decimal(18,2)");
            });

            // Configure Buyer entity
            modelBuilder.Entity<Buyer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Person)
                      .WithOne(p => p.Buyer)
                      .HasForeignKey<Buyer>(b => b.PersonId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // Configure ImportInfo entity
            modelBuilder.Entity<ImportInfo>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FileName).IsRequired().HasMaxLength(255);
                entity.Property(e => e.TotalRecords).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.ImportType).IsRequired();
                entity.Property(e => e.ImportDate).IsRequired();
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.User)
                      .WithMany(u => u.RefreshTokens)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.Property(e => e.TokenHash).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Expires).IsRequired();
                entity.Property(e => e.Created).IsRequired();
                entity.Property(e => e.CreatedByIp).IsRequired().HasMaxLength(45);
                entity.Property(e => e.Revoked).IsRequired(false);
                entity.Property(e => e.RevokedByIp).IsRequired(false).HasMaxLength(45);
            });

            modelBuilder.Entity<AttendanceDay>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.EmployeeId).IsRequired();
                entity.Property(e => e.Date).IsRequired();
                entity.HasMany(e => e.Punches)
                      .WithOne()
                      .HasForeignKey(p => p.AttendanceDayId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<AttendancePunch>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PunchTime).IsRequired();
                entity.Property(e => e.Type).IsRequired();
                entity.Property(e => e.Source).IsRequired();
                entity.Property(e => e.IsDeleted).IsRequired();
                entity.Property(e => e.EditReason).HasMaxLength(500);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.InvoiceNumber).IsRequired().HasMaxLength(50);
                entity.Property(e => e.InvoiceDate).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}