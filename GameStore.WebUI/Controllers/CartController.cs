using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        IGameRepository gameRepo;

        public CartController(IGameRepository gameRepository)
        {
            gameRepo = gameRepository;
        }

        public RedirectToRouteResult AddToCart(int gameId, string returnUrl)
        {
            Game game = gameRepo.Games.FirstOrDefault(g => g.GameId == gameId);
            if (game != null)
            {
                GetCart().AddItem(game, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int gameId, string returnUrl)
        {
            Game game = gameRepo.Games.FirstOrDefault(g => g.GameId == gameId);
            if (game != null)
            {
                GetCart().RemoveLine(game);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
    }
}