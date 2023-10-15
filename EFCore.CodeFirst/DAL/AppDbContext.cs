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
        public DbSet<ProductJoin> ProductJoins { get; set; }
        public DbSet<ProductList> ProductList { get; set; }

        //Base Class'ı DbSet olarak eklemiyoruz. eğer  public DbSet<BasePerson> Persons da eklemiş olsaydık o zaman Managers ve Employees tabloları oluşmayacak, tüm özellikler Persons tablosunda toplanacaktı 
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        //public DbSet<BasePerson> People { get; set; }

        //Owned Entity Types için DbSet<BasePerson> People'ı kaldırıyoruz

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Initializer.Build();
            //optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information).UseLazyLoadingProxies().UseSqlServer(Initializer.Configuration.GetConnectionString("ConStr"));

            optionsBuilder
                .LogTo(Console.WriteLine, LogLevel.Information)
                .UseSqlServer(Initializer.Configuration.GetConnectionString("ConStr"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking); //global düzeyde sorguların track edilmesini önler. AsNoTracking() yazmamıza gerek kalmaz 

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
            #region FLUENT API ile tablo adı değiştirme
            //modelBuilder.Entity<Product>().ToTable("ProductTBB","ProductBbb");
            //modelBuilder.Entity<Product>().HasKey(x=>x.Id); //tablonun primary key alanını belirler
            //modelBuilder.Entity<Product>().Property(x => x.Name).IsRequired();
            //modelBuilder.Entity<Product>().Property(x => x.Description).HasMaxLength(255);
            //modelBuilder.Entity<Product>().Property(x => x.Description).IsRequired().HasMaxLength(255).IsFixedLength(); //max ve min 255 karakter olabilceğini belirtiyor
            #endregion

            #region one to many
            //       modelBuilder.Entity<Category>()
            //.HasMany(x => x.Products)
            //.WithOne(x => x.Category)
            //.HasForeignKey(x => x.CategoryId);
            #endregion

            #region one to one
            /* modelBuilder.Entity<Product>()
     .HasOne(x => x.ProductFeature)
     .WithOne(x => x.Product)
     .HasForeignKey<ProductFeature>(x => x.ProductId); //Foreign Key olacak tabloyu Generic olarak belirtmemiz gerekiyor */

            //one to one ilişkide child tabloda primary key ve foreign keyi aynı tutabiliriz (best practice)
            /*modelBuilder.Entity<Product>()
                   .HasOne(x => x.ProductFeature2)
                   .WithOne(x => x.Product)
                   .HasForeignKey<ProductFeature2>(x => x.Id);*/
            #endregion

            #region many to many
            /* modelBuilder.Entity<Student>()
                 .HasMany(x => x.Teachers)
                 .WithMany(x => x.Students)
                 .UsingEntity<Dictionary<string, object>>(
                 "StudentTeacherManyToMany",
                 x => x.HasOne<Teacher>().WithMany().HasForeignKey("Teacher_Id").HasConstraintName("FK_TeacherId"),
                 x => x.HasOne<Student>().WithMany().HasForeignKey("Student_Id").HasConstraintName("FK_StudentId")
                 );*/
            #endregion

            #region Precision fluent api
            //modelBuilder.Entity<Product>().Property(x => x.Price).HasPrecision(15,2)
            #endregion

            #region HasComputedColumnSql
            //Price ve Kdv bilgisini çarğarak veritabanına yazar
            modelBuilder.Entity<Product>().Property(x => x.PriceKdv).HasComputedColumnSql("[Price]*[Kdv]");
            #endregion

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

            #region Keyless Entity Types
            //modelBuilder.Entity<ProductJoin>().HasNoKey();

            modelBuilder.Entity<ProductList>().HasNoKey();

            //veri tabanına yansıtmamayı sağlarız
            modelBuilder.Entity<ProductJoin>().ToTable(nameof(ProductJoin), t => t.ExcludeFromMigrations());
            modelBuilder.Entity<ProductList>().ToTable(nameof(ProductList), t => t.ExcludeFromMigrations());
            #endregion

            #region Entity Properties
            //modelBuilder.Entity<Category>().Ignore(x => x.Description); //NotMapped
            //modelBuilder.Entity<Category>().Property(x => x.Url).IsUnicode(false); //varchar
            //modelBuilder.Entity<Category>().Property(x => x.Name).HasColumnType("nvarchar(200)");
            #endregion

            #region Indexes
            //modelBuilder.Entity<Product>().HasIndex(x => x.Name);
            //modelBuilder.Entity<Product>().HasIndex(x => new { x.Price, x.Name});

            //included index
            //modelBuilder.Entity<Product>().HasIndex(x => x.Name).IncludeProperties(x => new { x.Price, x.Stock });
            #endregion

            #region HasCheckConstraint
            //modelBuilder.Entity<Product>().HasCheckConstraint("PriceConstraint", "[Price]>[DiscountPrice]");
            modelBuilder.Entity<Product>().ToTable(X => X.HasCheckConstraint("CK_Prices", "[Price] > [DiscountPrice]"));
            #endregion

            #region ToSqlQuery
            //custom class kullanarak her seferindse select sorgusu yazmamak için kullanırız
            //modelBuilder.Entity<CustomClass>().HasNoKey().ToSqlQuery("Select Name,Price From Products");
            #endregion

            #region ToView
            modelBuilder.Entity<ProductList>().ToView("ProductList");
            #endregion

            #region Global Query Filters
            modelBuilder.Entity<Product>().Property(x => x.IsDeleted).HasDefaultValue(false);
            //her seferinde sorguya isdeleted dahil etmemize gerek kalmıyor
            modelBuilder.Entity<Product>().HasQueryFilter(x => !x.IsDeleted);
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
