using GameStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.WebUI.Controllers
{
    public class NavController : Controller
    {

        IGameRepository gameRepo;

        public NavController(IGameRepository repository)
        {
            gameRepo = repository;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = gameRepo.Games.Select(g => g.Category).Distinct().OrderBy(c => c);
            return PartialView(categories);
        }
    }
}