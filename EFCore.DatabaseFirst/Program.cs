// See https://aka.ms/new-console-template for more information
using EFCore.DatabaseFirst.DAL;
using Microsoft.EntityFrameworkCore;

//Bir kere çalıştırmamız gerekir çünkü veri tabanı yoluna erişilmesi gerekir
//appsettings.json dosyasının copy to outputunu copy always veya Copy if newer yapmalıyız
DbContextInitializer.Build();

//İşlem bitince AppDbContext Dispose olması using kullanıyoruz
//using (var _context = new AppDbContext(DbContextInitializer.OptionsBuilder.Options))
//{
//    var products = await _context.Products.ToListAsync();

//    products.ForEach(x => Console.WriteLine($"{x.Name} - {x.Price}"));
//}

using (var _context = new AppDbContext())
{
    var products = await _context.Products.ToListAsync();

    products.ForEach(x => Console.WriteLine($"{x.Name} - {x.Price} ({x.Stock})"));
}