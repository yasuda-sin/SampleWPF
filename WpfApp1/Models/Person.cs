using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    internal class Person
    {
        public string FamilyName { get; set; }
        public string FirstName { get; set; }
        public string FullName { get { return FamilyName + FirstName; } }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
