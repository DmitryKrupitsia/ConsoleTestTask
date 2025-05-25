using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HysConsoleTestTask.Models;

public partial class HysTestTaskDbContext : DbContext
{
    public HysTestTaskDbContext()
    {
    }

    public HysTestTaskDbContext(DbContextOptions<HysTestTaskDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<DslProduct> DslProducts { get; set; }

    public virtual DbSet<TvProduct> TvProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC07F531BA0E");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
        });

        modelBuilder.Entity<DslProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DslProdu__3214EC07820E2849");

            entity.Property(e => e.Product).HasMaxLength(50);
        });

        modelBuilder.Entity<TvProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TvProduc__3214EC07683A3967");

            entity.Property(e => e.Product).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
