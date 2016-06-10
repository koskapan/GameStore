using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moq;
using Ninject;
using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;

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
            Mock<IGenericRepository<Game>> gameRepoMock = new Mock<IGenericRepository<Game>>();
            gameRepoMock.Setup(m => m.Entities).Returns(new List<Game>
            {
                new Game() { Name = "SimCity", Price = 1499 },
                new Game() { Name  = "TITANFALL", Price = 2299 },
                new Game() { Name = "Battlefield 4", Price = 899.4M }
            });
            kernel.Bind(typeof(IGenericRepository<>)).ToConstant(gameRepoMock.Object);
        }
    }
}