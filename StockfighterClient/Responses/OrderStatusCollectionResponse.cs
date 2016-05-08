using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockfighterClient.Responses
{
    public class OrderStatusCollectionResponse : BaseResponse
    {
        public string Venue { get; set; }
        public List<OrderStatusResponse> Orders { get; set; }
    }
}
