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
    public class GameController : Controller
    {
        IGameRepository repository;
        public int pageSize = 4;

        public GameController(IGameRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public int GetGamesCount(string category = "")
        {
            return category == "" ? repository.Games.Count() : repository.Games.Where(g => g.Category == category).Count();
        }

        [HttpGet]
        public IEnumerable<Game> Get(string category = "", int page = 1)
        {
            return category == "" ? repository.Games.Skip((page - 1)* pageSize).Take(pageSize) : repository.Games.Where(g => g.Category == category).Skip((page - 1) * pageSize).Take(pageSize);
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
