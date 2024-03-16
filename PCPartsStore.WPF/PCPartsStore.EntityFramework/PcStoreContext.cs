using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PCPartsStore.EntityFramework.DTOs;

namespace PCPartsStore.EntityFramework;

public partial class PcStoreContext : DbContext
{

    public PcStoreContext(DbContextOptions options)
        : base(options)
    {
    }

    public virtual DbSet<ArticleDTO> Articles { get; set; }

    public virtual DbSet<ArticleHasSupplierDTO> ArticleHasSuppliers { get; set; }

    public virtual DbSet<CategoryDTO> Categories { get; set; }

    public virtual DbSet<ItemDTO> Items { get; set; }

    public virtual DbSet<ManufacturerDTO> Manufacturers { get; set; }

    public virtual DbSet<PurchaseDTO> Purchases { get; set; }

    public virtual DbSet<ReceiptDTO> Receipts { get; set; }

    public virtual DbSet<SellerDTO> Sellers { get; set; }

    public virtual DbSet<SettingDTO> Settings { get; set; }

    public virtual DbSet<SupplierDTO> Suppliers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ArticleDTO>(entity =>
        {
            entity.HasKey(e => e.ArticleId).HasName("PRIMARY");

            entity.ToTable("article");

            entity.HasIndex(e => e.Name, "Name_UNIQUE").IsUnique();

            entity.HasIndex(e => e.CategoryId, "fk_article_category_idx");

            entity.HasIndex(e => e.ManufacturerId, "fk_article_manufacturer_idx");

            entity.Property(e => e.ManufacturingYear).HasColumnType("year");
            entity.Property(e => e.Name).HasMaxLength(32);
            entity.Property(e => e.Price).HasPrecision(6);

            entity.HasOne(d => d.Category).WithMany(p => p.Articles)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_article_category");

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.Articles)
                .HasForeignKey(d => d.ManufacturerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_article_manufacturer");
        });

        modelBuilder.Entity<ArticleHasSupplierDTO>(entity =>
        {
            entity.HasKey(e => new { e.ArticleId, e.SupplierId }).HasName("PRIMARY");

            entity.ToTable("article_has_supplier");

            entity.HasIndex(e => e.ArticleId, "fk_article_has_supplier_article_idx");

            entity.HasIndex(e => e.SupplierId, "fk_article_has_supplier_supplier_idx");

            entity.Property(e => e.PurchasePrice).HasPrecision(6);

            entity.HasOne(d => d.Article).WithMany(p => p.ArticleHasSuppliers)
                .HasForeignKey(d => d.ArticleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_article_has_supplier_article");

            entity.HasOne(d => d.Supplier).WithMany(p => p.ArticleHasSuppliers)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_article_has_supplier_supplier");
        });

        modelBuilder.Entity<CategoryDTO>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PRIMARY");

            entity.ToTable("category");

            entity.HasIndex(e => e.Name, "Name_UNIQUE").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(32);
        });

        modelBuilder.Entity<ItemDTO>(entity =>
        {
            entity.HasKey(e => new { e.ReceiptId, e.ArticleId }).HasName("PRIMARY");

            entity.ToTable("item");

            entity.HasIndex(e => e.ArticleId, "fk_item_article_idx");

            entity.HasIndex(e => e.ReceiptId, "fk_item_purchase_idx");

            entity.Property(e => e.Price).HasPrecision(7);

            entity.HasOne(d => d.Article).WithMany(p => p.Items)
                .HasForeignKey(d => d.ArticleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_item_article");

            entity.HasOne(d => d.Receipt).WithMany(p => p.Items)
                .HasForeignKey(d => d.ReceiptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_item_receipt");
        });

        modelBuilder.Entity<ManufacturerDTO>(entity =>
        {
            entity.HasKey(e => e.ManufacturerId).HasName("PRIMARY");

            entity.ToTable("manufacturer");

            entity.HasIndex(e => e.Name, "Name_UNIQUE").IsUnique();

            entity.Property(e => e.Country).HasMaxLength(64);
            entity.Property(e => e.Name).HasMaxLength(32);
        });

        modelBuilder.Entity<PurchaseDTO>(entity =>
        {
            entity.HasKey(e => e.PurchaseId).HasName("PRIMARY");

            entity.ToTable("purchase");

            entity.HasIndex(e => e.ArticleId, "fk_article_has_supplier_article_idx");

            entity.HasIndex(e => e.SupplierId, "fk_article_has_supplier_supplier_idx");

            entity.Property(e => e.Price).HasPrecision(6);

            entity.HasOne(d => d.Article).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.ArticleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_article_has_supplier_article1");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_article_has_supplier_supplier1");
        });

        modelBuilder.Entity<ReceiptDTO>(entity =>
        {
            entity.HasKey(e => e.ReceiptId).HasName("PRIMARY");

            entity.ToTable("receipt");

            entity.HasIndex(e => e.SellerId, "fk_receipt_seller_idx");

            entity.Property(e => e.TotalPrice).HasPrecision(7);

            entity.HasOne(d => d.Seller).WithMany(p => p.Receipts)
                .HasForeignKey(d => d.SellerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_receipt_seller");
        });

        modelBuilder.Entity<SellerDTO>(entity =>
        {
            entity.HasKey(e => e.SellerId).HasName("PRIMARY");

            entity.ToTable("seller");

            entity.HasIndex(e => e.Username, "Username_UNIQUE").IsUnique();

            entity.Property(e => e.PasswordHash).HasMaxLength(512);
            entity.Property(e => e.Username).HasMaxLength(32);
        });

        modelBuilder.Entity<SettingDTO>(entity =>
        {
            entity.HasKey(e => e.SellerId).HasName("PRIMARY");

            entity.ToTable("settings");

            entity.HasOne(d => d.Seller).WithOne(p => p.Setting)
                .HasForeignKey<SettingDTO>(d => d.SellerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_settings_seller");
        });

        modelBuilder.Entity<SupplierDTO>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PRIMARY");

            entity.ToTable("supplier");

            entity.Property(e => e.Location).HasMaxLength(64);
            entity.Property(e => e.SupplierName).HasMaxLength(32);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
