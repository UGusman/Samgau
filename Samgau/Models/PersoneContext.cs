using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samgau.Models
{
    public class PersoneContext : DbContext
    {
        public DbSet<Persone> persones { get; set; }
        public PersoneContext(DbContextOptions options) : base(options)
        {
        }
    }
}
