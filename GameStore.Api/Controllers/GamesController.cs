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

        [HttpGet]
        public IEnumerable<Game> Get()
        {
            return repository.Games;
        }

        [HttpGet("{id:int}")]
        public Game Get(int id)
        {
            return repository.Games.FirstOrDefault(g => g.GameId == id);
        }

        [HttpGet("Page{page_index:int}")]
        [HttpGet("{category}/{page_index?}")]
        public IEnumerable<Game> Get(string category = "", int page_index = 1, int page_size = 4)
        {
            return category == "" ? repository.Games.Skip((page_index - 1) * page_size).Take(page_size) : repository.Games.Where(g => g.Category == category).Skip((page_index - 1) * page_size).Take(page_size);
        }

    }
}
