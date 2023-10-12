using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.DAL
{
    //[Table("ProductTb", Schema = "products")] //tablo adını ProductTb yapmış oluruz
    public class Product
    {
        //[Key] primary key için 
        public int Id { get; set; }

        /*[Column("Name2", TypeName = "nvarchar(100)", Order = 3)] //kolon adını değiştiriyoruz, tipini belirliyoruz, tablodaki sırasını belirliyoruz*/
        //Order sadece ilk kez oluşan tablo için geçerlidir
        //[Required]
        public string Name { get; set; }

        //[MaxLength(255)]
        public string Description { get; set; }

        //[Column("PriceValue", TypeName = "decimal(15,2)", Order = 2)]
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Barcode { get; set; }
        public DateTime? CreatedDate { get; set; }

        //One to Many Convension

        public Category Category { get; set; } //navigation property
        public int CategoryId { get; set; }

        public ProductFeature ProductFeature { get; set; }

        //one to one ilişkide child tabloda primary key ve foreign keyi aynı tutabiliriz (best practice)
        public ProductFeature2 ProductFeature2 { get; set; }
    }
}
