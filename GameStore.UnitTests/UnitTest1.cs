using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameStore.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using System.Collections;
using System.Web.Mvc;
using GameStore.WebUI.Models;
using GameStore.WebUI.HtmlHelpers;

namespace GameStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            Mock<IGameRepository> gameRepoMock = new Mock<IGameRepository>();
            gameRepoMock.Setup(r => r.Games).Returns(new List<Game>
            {
                new Game() { GameId = 1, Name = "Game1" },
                new Game() { GameId = 2, Name = "Game2" },
                new Game() { GameId = 3, Name = "Game3" },
                new Game() { GameId = 4, Name = "Game4" },
                new Game() { GameId = 5, Name = "Game5" },
                new Game() { GameId = 6, Name = "Game6" }
            });

            GameController gameController = new GameController(gameRepoMock.Object);
            gameController.pageSize = 4;

            GamesListViewModel result = (GamesListViewModel)gameController.List(null,2).Model;

            List<Game> games = result.Games.ToList();
            Assert.IsTrue(games.Count == 2);
            Assert.AreEqual("Game5", games[0].Name);
            Assert.AreEqual("Game6", games[1].Name);

        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            HtmlHelper myHelper = null;

            PagingInfo pagingInfo = new PagingInfo()
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            Func<int, string> pageUrlDelegate = i => "Page" + i;

            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>", result.ToString());
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            Mock<IGameRepository> gameRepoMock = new Mock<IGameRepository>();
            gameRepoMock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game() { GameId = 1, Name = "Game1" },
                new Game() { GameId = 2, Name = "Game2" },
                new Game() { GameId = 3, Name = "Game3" },
                new Game() { GameId = 4, Name = "Game4" },
                new Game() { GameId = 5, Name = "Game5" }
            });

            GameController controller = new GameController(gameRepoMock.Object);
            controller.pageSize = 3;

            GamesListViewModel result = (GamesListViewModel)controller.List(null,2).Model;
            PagingInfo pageInfo = result.pagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);

        }

        [TestMethod]
        public void Can_Filter_Games()
        {
            Mock<IGameRepository> gameRepoMock = new Mock<IGameRepository>();
            gameRepoMock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game() { GameId = 1, Name = "Game1", Category="Cat1" },
                new Game() { GameId = 2, Name = "Game2", Category="Cat2" },
                new Game() { GameId = 3, Name = "Game3", Category="Cat1" },
                new Game() { GameId = 4, Name = "Game4", Category="Cat3" },
                new Game() { GameId = 5, Name = "Game5", Category="Cat2" }
            });

            GameController controller = new GameController(gameRepoMock.Object);

            List<Game> result = ((GamesListViewModel)controller.List("Cat1", 1).Model).Games.ToList();

            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result[0].Name == "Game2" && result[0].Category == "Cat2");
            Assert.IsTrue(result[1].Name == "Game5" && result[1].Category == "Cat2");


        }
    }
}
