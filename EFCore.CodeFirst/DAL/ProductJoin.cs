using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.DAL
{
    [Keyless]
    public class ProductJoin
    {
        //SELECT P.Id 'Product_Id', C.Name 'CategoryName', P.Name, P.Price FROM Products PJOIN ProductFeatures PF ON PF.Id = P.IdJOIN Categories C ON C.Id = P.CategoryId

        //Product_Id yerine Id olsaydı [Keyless] dememize gerke yoktuç Fakat migrationda yansıtmamalıyız
        //modelBuilder.Entity<User>().ToTable(nameof(Users), t => t.ExcludeFromMigrations());
        public int Product_Id { get; set; }
        public string CategoryName { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
