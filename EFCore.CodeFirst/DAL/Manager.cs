using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.DAL
{
    //public class Manager : BasePerson
    //{
    //    public int Grade { get; set; }
    //}

    //Owned Entity Types
    public class Manager
    {
        public int Id { get; set; }
        public int Grade { get; set; }
        public Person Person { get; set; }
    }
}
