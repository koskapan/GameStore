using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;
using GameStore.Api.Controllers;
using Moq;
using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using GameStore.Domain.Concrete;

namespace GameStore.Api.Test
{
    public class ApiUnitTest
    {
        [Fact]
        public void WebApi_CanPaginate()
        {
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(r => r.Games).Returns(new List<Game>
            {
                new Game { GameId = 1, Name = "Game1" },
                new Game { GameId = 2, Name = "Game2" },
                new Game { GameId = 3, Name = "Game3" },
                new Game { GameId = 4, Name = "Game4" },
                new Game { GameId = 5, Name = "Game5" },
                new Game { GameId = 6, Name = "Game6" },
                new Game { GameId = 7, Name = "Game7" }
            });
            GameController controller = new GameController(mock.Object);

            List<Game> result = controller.Get("", 2).ToList();

            Assert.Equal(3, result.Count);
            Assert.Equal(5, result[0].GameId);
        }

    }
}
