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
        IGenericRepository<Game> gameRepo;
        public GameController(IGenericRepository<Game> gamesRepository)
        {
            gameRepo = gamesRepository;
        }
        
        public ActionResult List()
        {
            return View(gameRepo.Entities);
        }
    }
}