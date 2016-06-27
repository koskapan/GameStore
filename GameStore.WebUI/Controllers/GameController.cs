using GameStore.Domain.Abstract;
using GameStore.Domain.Concrete;
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
                Games = gameRepo.Games.Where(p => category == null || p.Category == category).OrderBy(g => g.GameId).Skip((page - 1) * pageSize).Take(pageSize),
                pagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ? gameRepo.Games.Count() : gameRepo.Games.Where(g => g.Category == category).Count(),
                },
                CurrentCategory = category
            };
            return View(model);
        }
    }
}