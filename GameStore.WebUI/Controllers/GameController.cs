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
        public GameController(IGameRepository gamesRepository)
        {
            gameRepo = gamesRepository;
        }
        
        public ActionResult List()
        {
            return View(gameRepo.Games);
        }
    }
}