using Microsoft.EntityFrameworkCore;
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
        [Precision(15, 2)] //ondalık belirleme attribute
        public decimal Price { get; set; }

        public int Stock { get; set; }

        [Precision(15,2)] //ondalık belirleme attribute
        public decimal Kdv { get; set; }

        [Precision(15, 2)] //ondalık belirleme attribute
        public decimal PriceKdv { get; set; }
        public string Barcode { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //sadece insert çalışırken CreatedDate default olarak doldurulur. değer vermemiz gerek kalmaz
        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)] //hem update hem de inser yapıldığında LastAccessDate DateTime.Now; doldurulur
        public DateTime? LastAccessDate { get; set; } = DateTime.Now;

        //One to Many Convension

        public Category Category { get; set; } //navigation property
        public int CategoryId { get; set; }

        public ProductFeature ProductFeature { get; set; }

    }
}
