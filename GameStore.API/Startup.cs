using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using Moq;

namespace GameStore.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build().ReloadOnChanged("appsettings.json");
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddSingleton<IGameRepository>(s => CreateRepositoryMock());
            services.AddMvc();
        }

        private IGameRepository CreateRepositoryMock()
        {
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
            return gameRepoMock.Object;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIISPlatformHandler();

            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseMvc();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
