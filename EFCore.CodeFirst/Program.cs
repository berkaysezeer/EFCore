// See https://aka.ms/new-console-template for more information
using EFCore.CodeFirst;
using EFCore.CodeFirst.DAL;
using Microsoft.EntityFrameworkCore;

//appsetting.json okunabilir oluyor
Initializer.Build();

using (var context = new AppDbContext())
{
    ////Kalemler kategorisine kalem 1'i ekliyoruz
    //var category = new Category() { Name = "Kalemler" };

    #region 2. Yol One to many
    ////2.yol 
    ////category.Products.Add(new() { Name = "Kalem 1", Price = 100, Stock = 200, Barcode = "1000", Description = "Faber"});
    ////context.Add(category);
    #endregion

    #region 1. Yol One to many
    //var product = new Product() { Name = "Kalem 1", Price = 100, Stock = 200, Barcode = "1000", Description = "Faber", Category = category };

    ////kategoriyi de veri tabanına ekleyecek
    //context.Products.Add(product);
    ////context.Add(product);
    #endregion

    #region One to one
    ////1.yol
    //var product = new Product { Name = "Uçlu kalem", Price = 100, Stock = 200, Barcode = "1000", Description = "Faber", CategoryId = 1, ProductFeature = new() { Color = "Blue", Height = 100, Width = 100 } };
    //context.Products.Add(product);
    //context.SaveChanges();
    #endregion

    #region Many To Many
    //var student = new Student() { Age = 26, FullName = "Berkay Sezer" };
    //student.Teachers.Add(new() { Age = 24, FullName = "Ecem Bülbül" });
    //context.Add(student);

    //var teacher = new Teacher()
    //{
    //    FullName = "Berkay Sezer",
    //    Age = 26,
    //    Students = new List<Student>() {
    //new () { Age = 24,FullName = "Ali Veli"},
    //new () { Age = 24,FullName="Ayşe Fatma"}

    //}
    //};

    //context.Add(teacher);

    //var teacher = context.Teachers.First(x => x.FullName == "Berkay Sezer");

    //teacher.Students.AddRange(new List<Student>()
    //{
    //    new(){FullName="Fatma Kaya", Age =10},
    //    new(){FullName="Cevdet Nevale", Age = 19}
    //});

    //context.SaveChanges();
    #endregion


    Console.WriteLine("Kaydedildi");
}

#region Tracker
//using (var context = new AppDbContext())
//{
//    context.Products.Add(new Product { Name = "Kalem 1", Price = 10, Stock = 100, Barcode = "1330", Description = "Lorem ipsum" });
//    context.Products.Add(new Product { Name = "Kalem 2", Price = 10, Stock = 100, Barcode = "1331", Description = "Lorem ipsum" });
//    context.Products.Add(new Product { Name = "Kalem 2", Price = 10, Stock = 100, Barcode = "1331", Description = "Lorem ipsum" });

//    //her seferinde datetime crateddate girişi yapmıyoruz. Bunu daha merkezi bir yer olan changetracker içinde yapıyoruz. tek tek yazmamızdansa tek bir seferde halletmiş oluyoruz

//    context.SaveChanges();

//    //tüm veriler track edilir.
//    var products = await context.Products
//        .AsNoTracking() //sql tarafından gelen datanın ef core tarafına memoryde track edilmemesini sağlar
//        .ToListAsync();

//    //var products = await context.Products
//    //   .ToListAsync();

//    //products.ForEach(x =>
//    //{
//    //    x.Stock += 10;
//    //    //Console.WriteLine($"{x.Name} - {x.Price} ({x.Stock})");
//    //});

//    //eğer AsNoTracking() kullansaydık bu koşula girmeyecekti çünkü track edilmesini engellemiş olacaktık
//    context.ChangeTracker.Entries().ToList().ForEach(x =>
//    {
//        if (x.Entity is Product product)
//        {
//            Console.WriteLine($"{product.Name} - {product.Price} ({product.Stock})");
//        }
//    });

//    await context.SaveChangesAsync();
//}

#endregion

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