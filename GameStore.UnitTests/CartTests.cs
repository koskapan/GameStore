using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using GameStore.WebUI.Controllers;
using GameStore.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GameStore.UnitTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void Can_Add_New_Lines()
        {
            Game game1 = new Game { GameId = 1, Name = "Game1" };
            Game game2 = new Game { GameId = 2, Name = "Game2" };

            Cart cart = new Cart();

            cart.AddItem(game1, 1);
            cart.AddItem(game2, 1);
            List<CartLine> results = cart.Lines.ToList();

            Assert.AreEqual(2, results.Count());
            Assert.AreEqual(game1, results[0].Game);
            Assert.AreEqual(game2, results[1].Game);
        }

        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            Game game1 = new Game { GameId = 1, Name = "Game1" };
            Game game2 = new Game { GameId = 2, Name = "Game2" };

            Cart cart = new Cart();

            cart.AddItem(game1, 1);
            cart.AddItem(game2, 1);
            cart.AddItem(game1, 5);

            List<CartLine> results = cart.Lines.OrderBy(g => g.Game.GameId).ToList();

            Assert.AreEqual(2, results.Count());
            Assert.AreEqual(6, results[0].Quantity);
            Assert.AreEqual(1, results[1].Quantity);

        }

        [TestMethod]
        public void Can_Remove_Item()
        {
            Game game1 = new Game { GameId = 1, Name = "Game1" };
            Game game2 = new Game { GameId = 2, Name = "Game2" };
            Game game3 = new Game { GameId = 3, Name = "Game3" };

            Cart cart = new Cart();


            cart.AddItem(game1, 1);
            cart.AddItem(game2, 4);
            cart.AddItem(game3, 2);
            cart.AddItem(game2, 1);

            cart.RemoveLine(game2);

            Assert.AreEqual(0, cart.Lines.Where(c => c.Game == game2).Count());
            Assert.AreEqual(2, cart.Lines.Count());
        }

        [TestMethod]
        public void Calculate_Total_Cart()
        {

            Game game1 = new Game { GameId = 1, Name = "Game1", Price = 100 };
            Game game2 = new Game { GameId = 2, Name = "Game2", Price = 55 };

            Cart cart = new Cart();

            cart.AddItem(game1, 1);
            cart.AddItem(game2, 1);
            cart.AddItem(game1, 5);

            decimal result = cart.ComputeTotalValue();

            Assert.AreEqual(655, result);
        }

        [TestMethod]
        public void Can_Clear_Contents()
        { 
            Game game1 = new Game { GameId = 1, Name = "Game1", Price = 100 };
            Game game2 = new Game { GameId = 2, Name = "Game2", Price = 55 };

            Cart cart = new Cart();

            cart.AddItem(game1, 1);
            cart.AddItem(game2, 1);
            cart.AddItem(game1, 5);

            cart.Clear();

            Assert.AreEqual(0, cart.Lines.Count());
        }

        [TestMethod]
        public void Can_Add_To_Cart()
        {
            Mock<IGameRepository> repo = new Mock<IGameRepository>();
            repo.Setup(r => r.Games).Returns(new List<Game>
            {
                new Game { GameId =1, Name = "Game1", Category = "Cat1" }
            }.AsQueryable());

            Cart cart = new Cart();

            CartController controller = new CartController(repo.Object);

            controller.AddToCart(cart, 1, null);

            Assert.AreEqual(1, cart.Lines.Count());
            Assert.AreEqual(1, cart.Lines.ToList()[0].Game.GameId);

        }

        [TestMethod]
        public void Add_Game_To_Cart_Goes_To_Cart_Screen()
        {

            Mock<IGameRepository> repo = new Mock<IGameRepository>();
            repo.Setup(r => r.Games).Returns(new List<Game>
            {
                new Game { GameId =1, Name = "Game1", Category = "Cat1" }
            }.AsQueryable());
            Cart cart = new Cart();
            CartController controller = new CartController(repo.Object);

            RedirectToRouteResult result = controller.AddToCart(cart, 1, "myUrl");

            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("myUrl", result.RouteValues["returnUrl"]);
        }

        [TestMethod]
        public void Can_View_Cart_Content()
        {
            Cart cart = new Cart();
            CartController controller = new CartController(null);

            CartIndexViewModel result = (CartIndexViewModel)controller.Index(cart, "myUrl").ViewData.Model;

            Assert.AreSame(cart, result.Cart);
            Assert.AreEqual("myUrl", result.ReturnUrl);
        }
    }
}
