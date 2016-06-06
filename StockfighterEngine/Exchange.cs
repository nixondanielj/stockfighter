using StockfighterClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockfighterEngine
{
    public class Exchange
    {
        public string Venue { get; set; }
        
        private Dictionary<string, Stock> Stocks { get; set; }

        public Exchange(string venue)
        {
            this.Venue = venue;
            Stocks = new Dictionary<string, Stock>();
            using(var api = new Client())
            {
                var task = api.GetHB(venue);
                task.Wait();
                if(!task.Result)
                {
                    throw new Exception("Invalid venue");
                }
            }
        }

        public Stock GetStock(string ticker)
        {
            if(!Stocks.ContainsKey(ticker))
            {
                Stocks[ticker] = new Stock(Venue, ticker);
            }
            return Stocks[ticker];
        }

    }
}
