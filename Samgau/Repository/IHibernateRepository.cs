using Samgau.Models;
using Samgau.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samgau.Repository
{
    public interface IHibernateRepository
    {
        List<PersonGetViewModel> Get();
        void Add(PersoneViewModel persone);
        void Edite(PersonEditeViewModel persone);
        void Delete(long id);
        PersoneViewModel FindById(long id);

    }
}
