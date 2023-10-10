// See https://aka.ms/new-console-template for more information


using EFCore.DatabaseFirstByScaffold.Models;
using Microsoft.EntityFrameworkCore;

using (var context = new EfcoreDatabaseFirstDbContext())
{
    var products = await context.Products.ToListAsync();

    products.ForEach(x => Console.WriteLine($"{x.Name} - {x.Price} ({x.Stock})"));
}