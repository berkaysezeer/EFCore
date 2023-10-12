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
        public DbSet<Category> Categories { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        public DbSet<ProductFeature2> ProductFeatures2 { get; set; }

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

            //one to many
            /*modelBuilder.Entity<Category>()
                 .HasMany(x => x.Products)
                 .WithOne(x => x.Category)
                 .HasForeignKey(x => x.CategoryId);*/

            //one to one
            /* modelBuilder.Entity<Product>()
                 .HasOne(x => x.ProductFeature)
                 .WithOne(x => x.Product)
                 .HasForeignKey<ProductFeature>(x => x.ProductId); //Foreign Key olacak tabloyu Generic olarak belirtmemiz gerekiyor */

            //one to one ilişkide child tabloda primary key ve foreign keyi aynı tutabiliriz (best practice)
            modelBuilder.Entity<Product>()
                  .HasOne(x => x.ProductFeature2)
                  .WithOne(x => x.Product)
                  .HasForeignKey<ProductFeature2>(x => x.Id);

            /* modelBuilder.Entity<Student>()
                 .HasMany(x => x.Teachers)
                 .WithMany(x => x.Students)
                 .UsingEntity<Dictionary<string, object>>(
                 "StudentTeacherManyToMany",
                 x => x.HasOne<Teacher>().WithMany().HasForeignKey("Teacher_Id").HasConstraintName("FK_TeacherId"),
                 x => x.HasOne<Student>().WithMany().HasForeignKey("Student_Id").HasConstraintName("FK_StudentId")
                 );*/
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
