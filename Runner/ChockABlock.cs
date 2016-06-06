using StockfighterClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Runner
{
    public class ChockABlock
    {
        const string EXCHANGE = "URHEX";
        const string STOCK = "IZFI";
        public async Task Run()
        {
            using (Client client = new Client())
            {
                var originalQuote = await client.GetQuote(EXCHANGE, STOCK);
                int target = 6322;
                int position = 0;
                while(position < 100000)
                {
                    var quote = await client.GetQuote(EXCHANGE, STOCK);
                    if (quote.Last <= target)
                    {
                        var order = await client.PostOrder(new OrderRequest()
                        {
                            Account = "CAM60536252",
                            IsBuy = true,
                            Price = (int)(quote.Last * .95),
                            Qty = (int)(quote.LastSize),
                            Stock = STOCK,
                            Type = OrderTypes.IOC,
                            Venue = EXCHANGE
                        });
                        position += order.TotalFilled;
                    }
                    Console.WriteLine("Position: {0}", position);
                    Thread.Sleep(500);
                }
            }
        }
    }
}
