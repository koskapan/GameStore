using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moq;
using Ninject;
using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using GameStore.Domain.Concrete;
using System.Configuration;

namespace GameStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            //kernel.Bind<IGameRepository>().To<EFGameRepository>();
            Mock<IGameRepository> gameRepoMock = new Mock<IGameRepository>();
            gameRepoMock.Setup(g => g.Games).Returns(new List<Game>
            {
                new Game() { GameId = 1, Name = "Game1", Description = "Some game 1", Price = 123, Category = "Cat1" },
                new Game() { GameId = 2, Name = "Game2", Description = "Some game 2", Price = 123, Category = "Cat2" },
                new Game() { GameId = 3, Name = "Game3", Description = "Some game 3", Price = 123, Category = "Cat3" },
                new Game() { GameId = 4, Name = "Game4", Description = "Some game 4", Price = 123, Category = "Cat3" },
                new Game() { GameId = 5, Name = "Game5", Description = "Some game 5", Price = 123, Category = "Cat2" },
                new Game() { GameId = 6, Name = "Game6", Description = "Some game 6", Price = 123, Category = "Cat1" },
                new Game() { GameId = 7, Name = "Game7", Description = "Some game 7", Price = 123, Category = "Cat2" },
                new Game() { GameId = 8, Name = "Game8", Description = "Some game 8", Price = 123, Category = "Cat3" }
            });

            kernel.Bind<IGameRepository>().ToConstant(gameRepoMock.Object);
            var emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };
            kernel.Bind<IOrderProcessor>().To<OrderProcessor>().WithConstructorArgument("emailSettings", emailSettings);
        }
    }
}