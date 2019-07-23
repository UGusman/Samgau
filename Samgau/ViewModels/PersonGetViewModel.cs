using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samgau.ViewModels
{
    public class PersonGetViewModel
    {
        public long Id { get; set; }
        public  string Iin { get; set; }
        public  string FirstName { get; set; }
        public  string LastName { get; set; }
        public  DateTime BirthDate { get; set; }
    }
}
