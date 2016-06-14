using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using GameStore.WebUI.Models;
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
        
        public ViewResult List(string category, int page = 1)
        {
            GamesListViewModel model = new GamesListViewModel
            {
                Games = gameRepo.Games.OrderBy(g => g.GameId).Skip((page - 1) * pageSize).Take(pageSize),
                pagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = gameRepo.Games.Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }
    }
}