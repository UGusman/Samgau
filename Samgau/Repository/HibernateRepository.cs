using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using NHibernate;
using Samgau.Models;
using Samgau.ViewModels;

namespace Samgau.Repository
{
    public class HibernateRepository : IHibernateRepository
    {
        private readonly ISession _session;

        public HibernateRepository(ISession session)
        {
            _session = session;
        }

        public void Add(PersoneViewModel value)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    _session.Save(new Persone
                    {
                        Iin = value.Iin,
                        FirstName = value.FirstName,
                        LastName = value.LastName,
                        BirthDate = value.BirthDate
                    });
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }

            }
        }

        public void Delete(long id)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    _session.Delete(new Persone
                    {
                        Id = id
                    });
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }

        public void Edite(PersonEditeViewModel persone)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    _session.Update(new Persone
                    {
                        Id = persone.Id,
                        FirstName = persone.FirstName,
                        LastName = persone.LastName,
                        Iin = persone.Iin,
                        BirthDate = persone.BirthDate
                    });
                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }

        public PersoneViewModel FindById(long id)
        {
            return _session.Query<Persone>().Where(x => x.Id == id).Select(x => new PersoneViewModel
            {
                Iin = x.Iin,
                FirstName = x.FirstName,
                LastName = x.LastName,
                BirthDate = x.BirthDate
            }).FirstOrDefault();
        }

        public List<PersonGetViewModel> Get()
        {
            return _session.Query<Persone>().Select(x => new PersonGetViewModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Iin = x.Iin,
                BirthDate = x.BirthDate
            }).ToList(); ;
        }
    }
}
