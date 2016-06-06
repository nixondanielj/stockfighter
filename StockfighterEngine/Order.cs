using StockfighterClient.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockfighterEngine
{
    public class Order
    {
        public long OrderId { get; set; }

        private OrderStatusResponse LastResponse { get; set; }

        public Task Refresh()
        {

        }
    }
}
