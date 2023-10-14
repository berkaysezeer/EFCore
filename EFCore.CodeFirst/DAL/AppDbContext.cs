using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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

        //Base Class'ı DbSet olarak eklemiyoruz. eğer  public DbSet<BasePerson> Persons da eklemiş olsaydık o zaman Managers ve Employees tabloları oluşmayacak, tüm özellikler Persons tablosunda toplanacaktı 
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        //public DbSet<BasePerson> People { get; set; }

        //Owned Entity Types için DbSet<BasePerson> People'ı kaldırıyoruz

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Initializer.Build();
            //optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information).UseLazyLoadingProxies().UseSqlServer(Initializer.Configuration.GetConnectionString("ConStr"));

            optionsBuilder.UseSqlServer(Initializer.Configuration.GetConnectionString("ConStr"));

            /*
            Trace
            Debug
            Information
            Warning
            Error
            Critical         
             */
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
            /*modelBuilder.Entity<Product>()
                   .HasOne(x => x.ProductFeature2)
                   .WithOne(x => x.Product)
                   .HasForeignKey<ProductFeature2>(x => x.Id);*/

            /* modelBuilder.Entity<Student>()
                 .HasMany(x => x.Teachers)
                 .WithMany(x => x.Students)
                 .UsingEntity<Dictionary<string, object>>(
                 "StudentTeacherManyToMany",
                 x => x.HasOne<Teacher>().WithMany().HasForeignKey("Teacher_Id").HasConstraintName("FK_TeacherId"),
                 x => x.HasOne<Student>().WithMany().HasForeignKey("Student_Id").HasConstraintName("FK_StudentId")
                 );*/

            //Precision fluent api
            //modelBuilder.Entity<Product>().Property(x => x.Price).HasPrecision(15,2)

            //Price ve Kdv bilgisini çarğarak veritabanına yazar
            modelBuilder.Entity<Product>().Property(x => x.PriceKdv).HasComputedColumnSql("[Price]*[Kdv]");

            #region DeleteBehavior
            ////eğer parent sinilirse child tablodaki categoryid alanı null hale gelir
            //modelBuilder.Entity<Category>().HasMany(x => x.Products).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.SetNull);

            ////eğer parent silinmeye çalışılırsa ve child tabloda kendisinin bir elemanı var ise hata döner
            //modelBuilder.Entity<Category>().HasMany(x => x.Products).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);

            ////eğer parent silinirse altındaki elemanlar da child tablodan silinir
            //modelBuilder.Entity<Category>().HasMany(x => x.Products).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region TPT ( Table-Per-Type)
            //her class için ayrı tablo oluşturur
            //modelBuilder.Entity<BasePerson>().ToTable("People");
            //modelBuilder.Entity<Employee>().ToTable("Employees");
            //modelBuilder.Entity<Manager>().ToTable("Managers");
            #endregion

            #region Owned Entity Types
            //modelBuilder.Entity<Manager>().OwnsOne(x => x.Person);
            //modelBuilder.Entity<Employee>().OwnsOne(x => x.Person, p =>
            //{
            //    p.Property(x => x.FirstName).HasColumnName("Name");
            //});
            #endregion

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
