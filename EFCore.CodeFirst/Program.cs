// See https://aka.ms/new-console-template for more information
using EFCore.CodeFirst;
using EFCore.CodeFirst.DAL;
using Microsoft.EntityFrameworkCore;

//appsetting.json okunabilir oluyor
Initializer.Build();

using (var context = new AppDbContext())
{
    var products = await context.Products.ToListAsync();

    products.ForEach(x => Console.WriteLine($"{x.Name} - {x.Price} ({x.Stock})"));
}