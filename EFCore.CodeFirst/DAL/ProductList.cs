using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.DAL
{
    public class ProductList
    {
        public string CategoryName { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
