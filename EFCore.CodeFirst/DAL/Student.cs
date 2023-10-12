using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.DAL
{
    public class Student
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string FullName { get; set; }
        public List<Teacher> Teachers { get; set; } = new(); //.Teachers.Add dediğimizde hata vermemesi için
    }
}
