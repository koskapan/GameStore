using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.WebUI.Controllers
{
    public class GameController : Controller
    {
        IGameRepository gameRepo;
        public int pageSize = 4;
        public GameController(IGameRepository gamesRepository)
        {
            gameRepo = gamesRepository;
        }
        
        public ViewResult List(int page = 1)
        {
            return View(gameRepo.Games.OrderBy(game => game.GameId).Skip((page -1 )*pageSize).Take(pageSize));
        }
    }
}