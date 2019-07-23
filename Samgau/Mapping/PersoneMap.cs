using FluentNHibernate.Mapping;
using Samgau.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samgau.Mapping
{
    public class PersoneMap : ClassMap<Persone>
    {
        public PersoneMap()
        {
            Id(x => x.Id);
            Map(x => x.Iin).Length(9);
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.BirthDate);
            Table("Persones");
        }
    }
}
