namespace GameStore.Domain.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GameStore.Domain.Concrete.EFDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GameStore.Domain.Concrete.EFDbContext context)
        {
            context.Games.AddOrUpdate(
                    new Entities.Game() { Name = "The Elder Scrolls V: Skyrim", Price = 1399, Description=  "Abuot Dragons. You can do anything you want but DON'T kill chickens!" },
                    new Entities.Game() { Name = "Battlefield 4", Price = 1999, Description = "War, war, war. Now it's between US and China" },
                    new Entities.Game() { Name = "Batman: Arkham Knight", Price = 2230, Description = "One crazy person kills other crazy people. He's batman" },
                    new Entities.Game() { Name = "Fallout 4", Price = 1234, Description = "Scavenger simulation" }
                );
        }
    }
}
