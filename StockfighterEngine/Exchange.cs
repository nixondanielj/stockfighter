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
        public string Symbol { get; set; }

        public Exchange(string symbol)
        {
            this.Symbol = symbol;
            using(var api = new Client())
            {
                var task = api.GetHB(symbol);
                task.Wait();
                if(!task.Result)
                {
                    throw new Exception("Invalid symbol");
                }
            }
        }

    }
}
