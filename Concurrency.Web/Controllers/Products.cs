using Concurrency.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Concurrency.Web.Controllers
{
    public class Products : Controller
    {
        private readonly AppDbContext _context;

        public Products(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> List()
        {
            return View(await _context.Products.ToListAsync());
        }

        public async Task<IActionResult> Update(int Id)
        {
            var product = await _context.Products.FindAsync(Id);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Product p)
        {
            try
            {
                _context.Products.Update(p);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.First();

                //kullanıcının girdiği değerler
                var currentProduct = entry.Entity as Product;

                //property listesi döner
                var databaseValues = entry.GetDatabaseValues();

                //property listesi döner
                var client = entry.CurrentValues;

                //eğer farklı bir kullanıcı değeri sildiyse diye kontrol yapıyoruz
                if (databaseValues == null)
                {
                    ModelState.AddModelError(string.Empty, "Bu ürün başka bir kullanıcı tarafından silindi");
                }
                else
                {
                    //veri tabanındaki değerler
                    var databaseProduct = databaseValues.ToObject() as Product;

                    ModelState.AddModelError(string.Empty, "Bu ürün başka bir kullanıcı tarafından güncellendi");
                    ModelState.AddModelError(string.Empty, $"Güncel değer: {databaseProduct.Name} {databaseProduct.Price} ({databaseProduct.Stock})");
                }

                return View(p);
            }

        }
    }
}
