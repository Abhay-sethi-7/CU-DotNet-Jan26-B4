using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NorthwindCatalog.Services.Models;

public partial class NorthwindContext : DbContext
{
    public NorthwindContext()
    {
    }

    public NorthwindContext(DbContextOptions<NorthwindContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Connection string will be configured in Program.cs
        if (!optionsBuilder.IsConfigured)
        {
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // =========================
        // Category Configuration
        // =========================
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId);

            entity.Property(e => e.CategoryId)
                .HasColumnName("CategoryID");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(15);

            entity.Property(e => e.Description)
                .HasColumnType("ntext");

            entity.Property(e => e.Picture)
                .HasColumnType("image");
        });

        // =========================
        // Product Configuration
        // =========================
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId);

            entity.Property(e => e.ProductId)
                .HasColumnName("ProductID");

            entity.Property(e => e.ProductName)
                .HasMaxLength(40);

            entity.Property(e => e.QuantityPerUnit)
                .HasMaxLength(20);

            entity.Property(e => e.UnitPrice)
                .HasColumnType("decimal(10,2)");

            entity.Property(e => e.UnitsInStock)
                .HasDefaultValue((short)0);

            entity.Property(e => e.UnitsOnOrder)
                .HasDefaultValue((short)0);

            entity.Property(e => e.ReorderLevel)
                .HasDefaultValue((short)0);

            entity.Property(e => e.CategoryId)
                .HasColumnName("CategoryID");

            entity.Property(e => e.SupplierId)
                .HasColumnName("SupplierID");

            // Relationship
            entity.HasOne(d => d.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Products_Categories");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}