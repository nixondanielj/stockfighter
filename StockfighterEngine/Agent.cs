using StockfighterClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockfighterEngine
{
    public class Agent
    {
        public string Account { get; set; }
        public Stock Stock { get; private set; }
        public Exchange Exchange { get; private set; }

        public Agent(string account, string venue, string stock)
        {
            this.Account = account;
            this.Exchange = new Exchange(venue);
            this.Stock = Exchange.GetStock(stock);
        }

        public async Task Buy(int qty, int price)
        {

        }

        public async Task Buy(OrderTypes orderType, int qty, int price)
        {
            var req = new OrderRequest()
            {
                Account = Account,
                Venue = Exchange.Venue,
                Stock = Stock.Symbol,
                Price = price,
                Qty = qty,
                IsBuy = true,
                Type = orderType
            };

        }
    }
}
