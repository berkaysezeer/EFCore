using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.DAL
{
    //public class Employee : BasePerson
    //{
    //    public decimal Salary { get; set; }
    //}

    //Owned Entity Types
    public class Employee
    {
        public int Id { get; set; }
        public decimal Salary { get; set; }
        public Person Person { get; set; }
    }
}
