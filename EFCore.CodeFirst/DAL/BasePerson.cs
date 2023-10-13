using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.DAL
{
    public class BasePerson
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
    }
}
