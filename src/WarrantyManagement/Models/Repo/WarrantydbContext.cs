using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WarrantyManagement.Models.Repo;

public partial class WarrantydbContext : DbContext
{
    public WarrantydbContext()
    {
    }

    public WarrantydbContext(DbContextOptions<WarrantydbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Entry> Entries { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Variation> Variations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("addresses_pkey");

            entity.ToTable("addresses");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Brgy)
                .HasMaxLength(200)
                .HasColumnName("brgy");
            entity.Property(e => e.City)
                .HasMaxLength(200)
                .HasColumnName("city");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("created_at");
            entity.Property(e => e.Region)
                .HasMaxLength(200)
                .HasColumnName("region");
            entity.Property(e => e.Street)
                .HasMaxLength(255)
                .HasColumnName("street");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Zipcode).HasColumnName("zipcode");

            entity.HasOne(d => d.User).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("brands_pkey");

            entity.ToTable("brands");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        });

        modelBuilder.Entity<Entry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("entries_pkey");

            entity.ToTable("entries");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("created_at");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Serialno)
                .HasMaxLength(255)
                .HasColumnName("serialno");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.VariationId).HasColumnName("variation_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.Entries)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_customer");

            entity.HasOne(d => d.Product).WithMany(p => p.Entries)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_product");

            entity.HasOne(d => d.Variation).WithMany(p => p.Entries)
                .HasForeignKey(d => d.VariationId)
                .HasConstraintName("fk_variation");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("products_pkey");

            entity.ToTable("products");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .HasColumnName("name");
            entity.Property(e => e.Sku)
                .HasMaxLength(255)
                .HasColumnName("sku");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_brand");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_supplier");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("statuses_pkey");

            entity.ToTable("statuses");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ConsigneeId).HasColumnName("consignee_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("created_at");
            entity.Property(e => e.EntryId).HasColumnName("entry_id");
            entity.Property(e => e.IsCurrent)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("is_current");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

            entity.HasOne(d => d.Consignee).WithMany(p => p.Statuses)
                .HasForeignKey(d => d.ConsigneeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_consignee");

            entity.HasOne(d => d.Entry).WithMany(p => p.Statuses)
                .HasForeignKey(d => d.EntryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_entry");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("suppliers_pkey");

            entity.ToTable("suppliers");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(30)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("phone");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("last_name");
            entity.Property(e => e.Mobile)
                .HasMaxLength(30)
                .HasColumnName("mobile");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_role");
        });

        modelBuilder.Entity<Variation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("variations_pkey");

            entity.ToTable("variations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("created_at");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

            entity.HasOne(d => d.Product).WithMany(p => p.Variations)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_product");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
