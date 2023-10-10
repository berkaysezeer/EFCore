using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.DatabaseFirst.DAL
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DbContextInitializer.Configuration.GetConnectionString("ConStr"));
        }

        //Farklı veri tabanlarına bağlanmak isteyebiliriz. Onun için DbContextOptions kullanıyoruz ki dışarıdan ayar yapabilelim.
        //base(options) -> miras aldığımız sınıfın yapıcı metoduna gçnderiyoruz
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        //parametre alan bir constructor tanılamdığımız zaman mutlaka defaultu da tanımlamamız gerekir
        public AppDbContext()
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
