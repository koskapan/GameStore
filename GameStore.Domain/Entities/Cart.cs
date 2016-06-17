using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Game game, int quantity)
        {
            CartLine existingLine = lineCollection.Where(lc => lc.Game.GameId == game.GameId).FirstOrDefault();
            if (existingLine != null)
            {
                existingLine.Quantity += quantity;
            }
            else
            {
                lineCollection.Add(new CartLine
                {
                    Quantity = quantity,
                    Game = game
                });
            }
        }

        public void RemoveLine(Game game)
        {
            lineCollection.RemoveAll(l => l.Game.GameId == game.GameId);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(l => l.Game.Price * l.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }

    public class CartLine
    {
        public Game Game { get; set; }

        public int Quantity { get; set; }
    }

}
