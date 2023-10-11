// See https://aka.ms/new-console-template for more information
using EFCore.CodeFirst;
using EFCore.CodeFirst.DAL;
using Microsoft.EntityFrameworkCore;

//appsetting.json okunabilir oluyor
Initializer.Build();


using (var context = new AppDbContext())
{
    context.Products.Add(new Product { Name = "Kalem 1", Price = 10, Stock = 100, Barcode = "1330" });
    context.Products.Add(new Product { Name = "Kalem 2", Price = 10, Stock = 100, Barcode = "1331" });
    context.Products.Add(new Product { Name = "Kalem 2", Price = 10, Stock = 100, Barcode = "1331" });

    //her seferinde datetime crateddate girişi yapmıyoruz. Bunu daha merkezi bir yer olan changetracker içinde yapıyoruz. tek tek yazmamızdansa tek bir seferde halletmiş oluyoruz

    context.ChangeTracker.Entries().ToList().ForEach(x =>
    {
        if (x.Entity is Product product)
        {
            if (x.State == EntityState.Added)
            {
                product.CreatedDate = DateTime.Now;
            }
        }
    });

    context.SaveChanges();

    //tüm veriler track edilir.
    var products = await context.Products
        .AsNoTracking() //sql tarafından gelen datanın ef core tarafına memoryde track edilmemesini sağlar
        .ToListAsync();

    //var products = await context.Products
    //   .ToListAsync();

    //products.ForEach(x =>
    //{
    //    x.Stock += 10;
    //    //Console.WriteLine($"{x.Name} - {x.Price} ({x.Stock})");
    //});

    //eğer AsNoTracking() kullansaydık bu koşula girmeyecekti çünkü track edilmesini engellemiş olacaktık
    context.ChangeTracker.Entries().ToList().ForEach(x =>
    {
        if (x.Entity is Product product)
        {
            Console.WriteLine($"{product.Name} - {product.Price} ({product.Stock})");
        }
    });

    await context.SaveChangesAsync();
}

#region States
//using (var context = new AppDbContext())
//{
//    //var products = await context.Products.ToListAsync();

//    //products.ForEach(x =>
//    //{
//    //    /*
//    //     Unchanged: veri tabanında bir listeleme yaptığımızda gelen state
//    //     */
//    //    var state = context.Entry(x).State;



//    //    Console.WriteLine($"{x.Name} - {x.Price} ({x.Stock}) --> State: {state}");
//    //});

//    var newProduct = new Product { Barcode = "1223", Price = 20, Stock = 10, Name = "Uç", Description = "0.5 uç" };

//    //Detached: ef core'un bilgisi dahilinde olmayan yani henüz bir işlem yapılmamış 
//    Console.WriteLine($"State 1: {context.Entry(newProduct).State}");
//    await context.AddAsync(newProduct); //context.Entry(newProduct).State = EntityState.Added;


//    //Added: veri tabanına ilgili veri eklendikten sonra oluşan state
//    Console.WriteLine($"State 2: {context.Entry(newProduct).State}");

//    await context.SaveChangesAsync();

//    //Unchanged: artık ver tabanındaki data ile buradaki nesne eşit olduğu için Unchanged
//    Console.WriteLine($"State 3: {context.Entry(newProduct).State}");

//    Console.WriteLine("----------------");

//    var product = await context.Products.FirstAsync();
//    //Unchanged: artık ver tabanındaki data ile buradaki nesne eşit olduğu için Unchanged
//    Console.WriteLine($"product State 1: {context.Entry(product).State}");

//    product.Stock = 1000;
//    //Modified:bir değişiklik yapıldığı için
//    Console.WriteLine($"product State 2: {context.Entry(product).State}");

//    //Unchanged: artık ver tabanındaki data ile buradaki nesne eşit olduğu için Unchanged
//    await context.SaveChangesAsync();
//    Console.WriteLine($"product State 3: {context.Entry(product).State}");

//    Console.WriteLine("----------------");

//    //Unchanged: artık ver tabanındaki data ile buradaki nesne eşit olduğu için Unchanged
//    Console.WriteLine($"product State 1: {context.Entry(product).State}");
//    context.Remove(product);

//    //deleted
//    Console.WriteLine($"product State 2: {context.Entry(product).State}");
//    await context.SaveChangesAsync();

//    //Detached: artık böyle bir nesne memory'de yok
//    Console.WriteLine($"product State 3: {context.Entry(product).State}");
//} 
#endregion