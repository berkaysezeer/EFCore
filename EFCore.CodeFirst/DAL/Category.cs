using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.DAL
{
    public class Category
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string Name { get; set; }

        [NotMapped] //veri tabanına yansımamasını sağlarız
        public string Description { get; set; }

        [Unicode(false)] //varchar olarak kaydedilmesini sağlar
        public string Url { get; set; }

        public virtual List<Product> Products { get; set; } = new List<Product>();
    }
}
