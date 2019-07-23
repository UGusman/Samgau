using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using Samgau.Models;
using Samgau.Repository;
using Samgau.ViewModels;

namespace Samgau.Controllers
{
    
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PersonController : ControllerBase
    {
        //private readonly IPersoneRepository _personeRepository;
        private readonly ISession _session;
        public PersonController(ISession session)
        {
            _session = session;
            //_personeRepository = personeRepository;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Persone>> Get()
        {
            return _session.Query<Persone>().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Persone> GetById(int id)
        {
            return _session.Query<Persone>().Where(x => x.Id == id).FirstOrDefault();
        }


        [HttpPost]
        public IActionResult Post([FromBody] PersoneViewModel value)
        {
            if (!ModelState.IsValid)
                return BadRequest();
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
            
            
            return Ok();
        }

        [HttpPut]
        public IActionResult Put([FromBody] Persone value)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    _session.Update(value);
                    _session.Transaction.Commit();
                    transaction.Commit();
                   
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
               
                
            }
           return Ok();

        }

        [HttpDelete("{id}")]
        public void Delete(int id)
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

        //[HttpGet]
        //public ActionResult<IEnumerable<Persone>> Get()
        //{
        //    return _personeRepository.Get();
        // }

        //[HttpGet("{id}")]
        //public ActionResult<Persone> GetById(int id)
        //{
        //    return _personeRepository.FindById(id);
        //}

        //[HttpPost]
        //public IActionResult Post([FromBody] PersoneViewModel value)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest();
        //    _personeRepository.Add(value);
        //    return Ok();
        //}

        //[HttpPut]
        //public IActionResult Put([FromBody] Persone value)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest();
        //    _personeRepository.Edite(value);
        //    return Ok();
        //}

        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //    _personeRepository.Delete(id);
        //}
    }
}
