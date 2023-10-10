using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.DatabaseFirst.DAL
{
    //veri tabanını ilgilendiren ayarları burada belirteceğiz
    /*API ve WEB uygulamalarında direk program.cs dosyası içerisinde service olarak ekliyoruz.. Yani DI Container'a ekliyoruz.
    builder.services.AddDbContext()
    Console uygulamalarında hazır DI Container gelmediği için bu şekilde kendimiz inşa ettik.*/

    public class DbContextInitializer
    {
        public static IConfigurationRoot Configuration; //appsettings.json dosyasını okuyabilmek için
        public static DbContextOptionsBuilder<AppDbContext> OptionsBuilder; //veri tabanı ile ilgili ayarları belirteceğimiz yer

        //Uygulama ayağa kalktığında metot bir kere çalışıp set edilmiş olacak *static
        public static void Build()
        {
            //Directory.GetCurrentDirectory() --> uygulamanın çalıştığı klasörü alır
            //optional:true --> appsettings.json dosyası olabilir de olmayabilir de demek
            //reloadOnChange:true --> dosyada ger değişiklik yaptığımızda yeniden yüklensin
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            //Okuyabileceğimiz dosyayı hazır hale getiriyoruz. Uygulamanın herhangi bir yerinde appsettings içerisindeki key value değerlerini IConfigurationRoot Configuration ile okuyabileceğiz
            Configuration = builder.Build();

            //OptionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            ////appsettings.json dosyasından ConStr değerini aldık
            //OptionsBuilder.UseSqlServer(Configuration.GetConnectionString("ConStr"));
        }
    }
}
