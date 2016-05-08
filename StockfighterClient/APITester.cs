using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockfighterClient
{
    public class APITester
    {
        public async Task Test()
        {
            using (var api = new Client())
            {
                if(await api.GetHB())
                {
                    Console.WriteLine("API looks good");
                }
                if(await api.GetHB("TESTEX"))
                {
                    Console.WriteLine("TESTEX looks good");
                }
                var stocks = await api.GetStocks("TESTEX");
                if (stocks.Ok)
                {
                    Console.WriteLine("Stocks on TESTEX look good");
                    Console.WriteLine("Includes: [ {0} ]", string.Join(", ", stocks.Symbols.Select(s => s.Symbol)));
                }
                var orderBook = await api.GetOrderBook("TESTEX", "FOOBAR");
                if (orderBook.Ok)
                {
                    Console.WriteLine("Orderbook for FOOBAR on TESTEX looks good");
                }
                var fakeOrder = new OrderRequest()
                {
                    Account = "EXB123456",
                    IsBuy = true,
                    Type = OrderTypes.Limit,
                    Price = 1000,
                    Qty = 100,
                    Stock = "FOOBAR",
                    Venue = "TESTEX"
                };
                var orderPlaced = await api.PostOrder(fakeOrder);
                if (orderPlaced.Ok)
                {
                    Console.WriteLine("Fake order looks good");
                }
                if(orderPlaced.Open)
                {
                    var delResp = await api.DeleteOrder(orderPlaced.Id, orderPlaced.Venue, orderPlaced.Symbol);
                    if (delResp.Ok)
                    {
                        Console.WriteLine("Order delete looks good");
                    }
                }
                else
                {
                    Console.WriteLine("Could not test delete, order closed");
                }
                var quote = await api.GetQuote("TESTEX", "FOOBAR");
                if(quote.Ok)
                {
                    Console.WriteLine("Quote looks good");
                }
                var allOrders = await api.GetAllOrders("TESTEX", fakeOrder.Account);
                if(allOrders.Ok)
                {
                    Console.WriteLine("All orders looks good");
                }
                var allOrdersByStock = await api.GetAllOrders("TESTEX", fakeOrder.Account, fakeOrder.Stock);
                if (allOrders.Ok)
                {
                    Console.WriteLine("All orders by stock looks good");
                }
            }
            Console.ReadKey();
        }
    }
}
