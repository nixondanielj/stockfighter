using StockfighterClient;
using StockfighterClient.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockfighterEngine
{
    public class Stock
    {
        public int SharesOwned { get; private set; }
        public int Pending { get; private set; }
        public string Symbol { get; private set; }

        private Dictionary<long, OrderStatusResponse> MyOpenOrders { get; set; }

        internal Stock(string venue, string stock)
        {
            Symbol = stock;
        }

        public void Purchase(OrderRequest request)
        {
            using (var api = new Client())
            {
                api.PostOrder(request).ContinueWith(task => ProcessRequestCompletion(task.Result));
            }
        }

        private void ProcessRequestCompletion(OrderStatusResponse response)
        {
            if(MyOpenOrders.ContainsKey(response.Id))
            {
                SharesOwned -= MyOpenOrders[response.Id].TotalFilled;
                Pending -= MyOpenOrders[response.Id].Qty;
                if (!response.Open)
                {
                    MyOpenOrders.Remove(response.Id);
                }
            }
            if(response.Open)
            {
                MyOpenOrders[response.Id] = response;
            }
            SharesOwned += response.TotalFilled;
            Pending += response.Qty;
        }

        private async Task Refresh()
        {
            
        }
    }
}
