// See https://aka.ms/new-console-template for more information
using EFCore.CodeFirst;
using EFCore.CodeFirst.DAL;
using Microsoft.EntityFrameworkCore;

//appsetting.json okunabilir oluyor
Initializer.Build();

using (var context = new AppDbContext())
{
    //var products = await context.Products.ToListAsync();

    //products.ForEach(x =>
    //{
    //    /*
    //     Unchanged: veri tabanında bir listeleme yaptığımızda gelen state
    //     */
    //    var state = context.Entry(x).State;



    //    Console.WriteLine($"{x.Name} - {x.Price} ({x.Stock}) --> State: {state}");
    //});

    var newProduct = new Product { Barcode = "1223", Price = 20, Stock = 10, Name = "Uç", Description = "0.5 uç" };

    //Detached: ef core'un bilgisi dahilinde olmayan yani henüz bir işlem yapılmamış 
    Console.WriteLine($"State 1: {context.Entry(newProduct).State}");
    await context.AddAsync(newProduct); //context.Entry(newProduct).State = EntityState.Added;


    //Added: veri tabanına ilgili veri eklendikten sonra oluşan state
    Console.WriteLine($"State 2: {context.Entry(newProduct).State}");

    await context.SaveChangesAsync();

    //Unchanged: artık ver tabanındaki data ile buradaki nesne eşit olduğu için Unchanged
    Console.WriteLine($"State 3: {context.Entry(newProduct).State}");

    Console.WriteLine("----------------");

    var product = await context.Products.FirstAsync();
    //Unchanged: artık ver tabanındaki data ile buradaki nesne eşit olduğu için Unchanged
    Console.WriteLine($"product State 1: {context.Entry(product).State}");

    product.Stock = 1000;
    //Modified:bir değişiklik yapıldığı için
    Console.WriteLine($"product State 2: {context.Entry(product).State}");

    //Unchanged: artık ver tabanındaki data ile buradaki nesne eşit olduğu için Unchanged
    await context.SaveChangesAsync();
    Console.WriteLine($"product State 3: {context.Entry(product).State}");

    Console.WriteLine("----------------");

    //Unchanged: artık ver tabanındaki data ile buradaki nesne eşit olduğu için Unchanged
    Console.WriteLine($"product State 1: {context.Entry(product).State}");
    context.Remove(product);

    //deleted
    Console.WriteLine($"product State 2: {context.Entry(product).State}");
    await context.SaveChangesAsync();

    //Detached: artık böyle bir nesne memory'de yok
    Console.WriteLine($"product State 3: {context.Entry(product).State}");
}