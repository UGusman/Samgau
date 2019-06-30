using Samgau.Models;
using Samgau.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samgau.Repository
{
    public interface IPersoneRepository
    {
        List<Persone> Get();
        void Add(PersoneViewModel persone);
        void Edite(Persone persone);
        void Delete(long id);
        Persone FindById(long id);
    }
}
 