using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using System.Linq.Expressions;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GameStore.Api.Controllers
{
    [Route("api/[controller]")]
    public class GamesController : Controller
    {
        IGameRepository repository;

        public GamesController(IGameRepository repository)
        {
            this.repository = repository;
        }
        

        // GET: api/values
        [HttpGet]
        public IEnumerable<Game> Get()
        {
            return repository.Games.ToList();
        }

        [HttpGet]
        public IEnumerable<Game> Get(Func<Game, bool> predicate)
        {
            return repository.Games.Where(predicate);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Game Get(int id)
        {
            return repository.Games.FirstOrDefault(g => g.GameId == id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Game value)
        {
            
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Game value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
