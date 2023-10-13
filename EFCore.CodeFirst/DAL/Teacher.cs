using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.DAL
{
    public class Teacher
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }

        public virtual List<Student> Students { get; set; } = new(); //.Students.Add dediğimizde hata vermemesi için
    }
}
