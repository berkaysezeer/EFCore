// See https://aka.ms/new-console-template for more information
using EFCore.CodeFirst;
using EFCore.CodeFirst.DAL;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

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

    #region Eager Loading
    //var categoryWithProducts = context.Categories.Include(c => c.Products).First();
    //var productsWithPF = context.Products.Include(c => c.ProductFeature).First();
    //var product = context.Products.Include(x => x.Category).Include(x => x.ProductFeature).First();
    #endregion

    #region Explicit Loading
    ////örn. senaryoda kategori içerisinde bulunan productlara öncesinde ihtiyacımız olmadı. Fakat sonradan ihtiyacımız olursa Explicit Loading kullanabiliriz
    //var category = context.Categories.First();
    ////
    ////
    ////
    //if (true)
    //{
    //    //1. yol
    //    var products = context.Products.Where(x => x.CategoryId == category.Id).ToList();

    //    //2.yol (best practice)
    //    context.Entry(category).Collection(x => x.Products).Load();
    //}

    //var product = context.Products.First();
    ////
    ////
    ////
    //if (true)
    //{
    //    //one to one ilişki olduğu için Reference kullanmamız gerekiyor
    //    context.Entry(product).Reference(x => x.ProductFeature).Load();
    //}
    #endregion

    #region Lazy Loading 
    //Microsoft.EntityFrameworkCore.Proxies

    //var category = await context.Categories.FirstAsync();
    //var products = category.Products;
    #endregion

    #region Owned Entity Types
    //var employee = new Employee()
    //{
    //    Salary = 5,
    //    Person = new Person() { Age = 1, FirstName = "Berkay", LastName = "Sezer" }
    //};

    //context.Employees.Add(employee);
    //context.SaveChanges();
    //Console.WriteLine("Kaydedildi");
    #endregion

    var productJoins = context.ProductJoins.FromSqlRaw("SELECT P.Id 'Product_Id', C.Name 'CategoryName', P.Name, P.Price FROM Products P JOIN ProductFeatures PF ON PF.Id = P.Id JOIN Categories C ON C.Id = P.CategoryId").ToList();

    #region Client vs Server Evaluation
    //Aşağıdaki sorgu RemoveSpace metodunu normal sql sorgusuna çevirmeye çalışacağı için hata veriyor (server evaluation). yani local metodlar bu şekilde kullanılamaz. bunun için öncelikle tolist kullanmalıyız
    //var products = context.Products.Where(x => RemoveSpace(x.Description) == "Uçlukalem").ToList();

    //var products = context.Products.ToList() --> server evaluation, RemoveSpace --> client evaluation
    //var products = context.Products.ToList().Where(x => RemoveSpace(x.Description) == "Uçlukalem").ToList();
    //var products = context.Products.ToList().Select(x => new { Description = RemoveSpace(x.Description), x.Name }).ToList();
    #endregion

    #region Inner Join
    //var result = context.Categories
    //    .Join(context.Products, x => x.Id, y => y.CategoryId, (c, p) => new
    //    {
    //        CategoryName = c.Name,
    //        CategoryDescription = c.Description,
    //        ProductName = p.Name,
    //        ProductPrice = p.Price
    //    }).ToList();

    //sadece product ile ilgili alanlar gelir
    //var resultv2 = context.Categories
    //    .Join(context.Products, x => x.Id, y => y.CategoryId, (c, p) => p).ToList();

    //3lü inner join
    //var resultv3 = context.Categories
    //   .Join(context.Products, x => x.Id, y => y.CategoryId, (c, p) => new { c, p })
    //   .Join(context.ProductFeatures, x => x.p.Id, y => y.Id, (c, pf) => new
    //   {

    //       CategoryName = c.c.Name,
    //       ProductName = c.p.Name,
    //       Color = pf.Color
    //   })
    //   .ToList();

    //2.yol
    //var resultWithLinqQuery = (from c in context.Categories
    //                     join p in context.Products on c.Id equals p.CategoryId
    //                     select new
    //                     {
    //                         CategoryName = c.Name,
    //                         CategoryDescription = c.Description,
    //                         ProductName = p.Name,
    //                         ProductPrice = p.Price
    //                     }).ToList();
    #endregion

    #region Left/Right Join
    //var resultLJ = await (from p in context.Products
    //                      join pf in context.ProductFeatures on p.Id equals pf.Id
    //                      into pflist
    //                      from pf in pflist.DefaultIfEmpty() //boi ise defaultu al
    //                      select new { p, pf }
    //                ).ToListAsync();

    //var resultRJ = await (from pf in context.ProductFeatures
    //                      join p in context.Products on pf.Id equals p.Id
    //                      into plist
    //                      from p in plist.DefaultIfEmpty() //boi ise defaultu al
    //                      select new { p, pf }
    //                ).ToListAsync();
    #endregion

    #region Raw Sql
    //var product = await context.Products.FromSqlRaw("SELECT * FROM PRODUCTS WHERE PRICE > {0}", 500).ToListAsync();

    //var productWithInterpolated = await context.Products.FromSqlInterpolated($"SELECT * FROM PRODUCTS WHERE PRICE > {500}").ToListAsync();
    #endregion

    #region ToView
    //var products = context.ProductList.ToList();
    #endregion
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

#region Pagination
//GetProducts(1, 6).ForEach(x =>
//{
//    Console.WriteLine($"{x.Id} - {x.Name}");
//});


static  List<Product> GetProducts(int page, int pageSize)
{
    using (var context = new AppDbContext())
    {
        //page 1 size = 2 ==> skip: 0 take: 2
        //page 2 size = 2 ==> skip: 2 take: 2
        //page 3 size = 2 ==> skip: 4 take: 2

        var products = context.Products
            .IgnoreQueryFilters() //filtreleri iptal edebiliyoruz
            .OrderByDescending(x => x.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        return products;
    }

}

#endregion

string RemoveSpace(string value)
{
    return value.Replace(" ", "");
}