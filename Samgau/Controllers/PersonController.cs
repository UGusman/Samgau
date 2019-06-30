using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IPersoneRepository _personeRepository;

        public PersonController(IPersoneRepository personeRepository)
        {
            _personeRepository = personeRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Persone>> Get()
        {
            return _personeRepository.Get();
         }

        [HttpGet("{id}")]
        public ActionResult<Persone> GetById(int id)
        {
            return _personeRepository.FindById(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody] PersoneViewModel value)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            _personeRepository.Add(value);
            return Ok();
        }

        [HttpPut]
        public IActionResult Put([FromBody] Persone value)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            _personeRepository.Edite(value);
            return Ok();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _personeRepository.Delete(id);
        }
    }
}
