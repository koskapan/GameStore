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

            IEnumerable<Game> result = (IEnumerable<Game>)gameController.List(2).Model;

            List<Game> games = result.ToList();
            Assert.IsTrue(games.Count == 2);
            Assert.AreEqual("Game5", games[0].Name);
            Assert.AreEqual("Game6", games[1].Name);

        }
    }
}
