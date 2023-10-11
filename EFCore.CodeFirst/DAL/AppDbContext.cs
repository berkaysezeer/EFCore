using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Initializer.Build();
            optionsBuilder.UseSqlServer(Initializer.Configuration.GetConnectionString("ConStr"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //FLUENT API ile tablo adı değiştirme
            //modelBuilder.Entity<Product>().ToTable("ProductTBB","ProductBbb");
            //modelBuilder.Entity<Product>().HasKey(x=>x.Id); //tablonun primary key alanını belirler
            //modelBuilder.Entity<Product>().Property(x => x.Name).IsRequired();
            //modelBuilder.Entity<Product>().Property(x => x.Description).HasMaxLength(255);
            //modelBuilder.Entity<Product>().Property(x => x.Description).IsRequired().HasMaxLength(255).IsFixedLength(); //max ve min 255 karakter olabilceğini belirtiyor
        }

        //public override int SaveChanges()
        //{
        //    ChangeTracker.Entries().ToList().ForEach(x =>
        //    {
        //        if (x.Entity is Product product)
        //        {
        //            if (x.State == EntityState.Added)
        //            {
        //                product.CreatedDate = DateTime.Now;
        //            }
        //        }
        //    });

        //    return base.SaveChanges();
        //}
    }
}
