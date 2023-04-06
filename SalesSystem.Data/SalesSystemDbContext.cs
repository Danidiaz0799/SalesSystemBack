using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SalesSystem.Core.Models;

namespace SalesSystem.Data;

public partial class SalesSystemDbContext : DbContext
{
    public SalesSystemDbContext()
    {
    }

    public SalesSystemDbContext(DbContextOptions<SalesSystemDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Advisor> Advisors { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Dealer> Dealers { get; set; }

    public virtual DbSet<Manager> Managers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Purchase> Purchases { get; set; }

    public virtual DbSet<PurchaseStatus> PurchaseStatuses { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<SalesStatus> SalesStatuses { get; set; }

    public virtual DbSet<Shop> Shops { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Advisor>(entity =>
        {
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.Identification).HasMaxLength(50);

            entity.HasOne(d => d.Shop).WithMany(p => p.Advisors)
                .HasForeignKey(d => d.ShopId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Advisors_Shops");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.Identification).HasMaxLength(50);
        });

        modelBuilder.Entity<Dealer>(entity =>
        {
            entity.Property(e => e.DealerName)
                .HasMaxLength(50)
                .HasColumnName("dealerName");
            entity.Property(e => e.Nit)
                .HasMaxLength(50)
                .HasColumnName("NIT");
        });

        modelBuilder.Entity<Manager>(entity =>
        {
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.Identification).HasMaxLength(50);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.ProductName).HasMaxLength(50);

            entity.HasOne(d => d.Dealer).WithMany(p => p.Products)
                .HasForeignKey(d => d.DealerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Dealers");
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(50);

            entity.HasOne(d => d.Dealer).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.DealerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchases_Dealers");

            entity.HasOne(d => d.Manager).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchases_Managers");

            entity.HasOne(d => d.PurchaseStatus).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.PurchaseStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchases_Purchase_Status");
        });

        modelBuilder.Entity<PurchaseStatus>(entity =>
        {
            entity.ToTable("Purchase_Status");

            entity.Property(e => e.PurchaseName).HasMaxLength(50);
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(50);

            entity.HasOne(d => d.Advisor).WithMany(p => p.Sales)
                .HasForeignKey(d => d.AdvisorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Advisors");

            entity.HasOne(d => d.Customer).WithMany(p => p.Sales)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Customers");

            entity.HasOne(d => d.SaleStatus).WithMany(p => p.Sales)
                .HasForeignKey(d => d.SaleStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Sales_Status");
        });

        modelBuilder.Entity<SalesStatus>(entity =>
        {
            entity.HasKey(e => e.SaleStatusId);

            entity.ToTable("Sales_Status");

            entity.Property(e => e.StatusName).HasMaxLength(50);
        });

        modelBuilder.Entity<Shop>(entity =>
        {
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.ShopName).HasMaxLength(50);

            entity.HasOne(d => d.Manager).WithMany(p => p.Shops)
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shops_Managers");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
