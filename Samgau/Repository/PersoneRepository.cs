using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Samgau.Models;
using Samgau.ViewModels;

namespace Samgau.Repository
{
    public class PersoneRepository : IPersoneRepository
    {
        private readonly PersoneContext _personeContext;

        public PersoneRepository(PersoneContext personeContext)
        {
            _personeContext = personeContext;
        }

        public void Add(PersoneViewModel persone)
        {
            try
            {
                _personeContext.persones.Add(new Persone { Iin= persone.Iin,
                    FirstName = persone.FirstName,
                    LastName = persone.LastName,
                    BirthDate = persone.BirthDate});
                _personeContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public void Delete(long id)
        {
            try
            {
                _personeContext.persones.RemoveRange(new Persone { Id = id});
                _personeContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public void Edite(Persone persone)
        {
            try
            {
                Persone originalPersone = _personeContext.persones.Find(persone.Id);
                originalPersone.Iin = persone.Iin;
                originalPersone.FirstName = persone.FirstName;
                originalPersone.LastName = persone.LastName;
                originalPersone.BirthDate = persone.BirthDate;
                _personeContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public Persone FindById(long id)
        {
            return _personeContext.persones.Find(id);
        }

        public List<Persone> Get()
        {
            return _personeContext.persones.ToList();
        }
    }
}
