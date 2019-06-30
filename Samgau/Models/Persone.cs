using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Samgau.Models
{
    public class Persone
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(12)]
        [RegularExpression("^[0-9]*$")]
        public string Iin { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Zа-яА-Я]*$")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Zа-яА-Я]*$")]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
    }
}
