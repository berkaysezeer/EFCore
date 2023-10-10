using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EFCore.DatabaseFirstByScaffold.Models;

public partial class EfcoreDatabaseFirstDbContext : DbContext
{
    public EfcoreDatabaseFirstDbContext()
    {
    }

    public EfcoreDatabaseFirstDbContext(DbContextOptions<EfcoreDatabaseFirstDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=EFCoreDatabaseFirstDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Product");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
