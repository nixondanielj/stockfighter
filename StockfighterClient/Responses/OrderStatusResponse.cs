using StockfighterClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockfighterClient.Responses
{
    public class OrderStatusResponse : BaseResponse
    {
        public string Symbol { get; set; }
        public string Venue { get; set; }
        public string Direction { get; set; }
        public int OriginalQty { get; set; }
        public int Qty { get; set; }
        public int Price { get; set; }
        public string OrderType { get; set; }
        public long Id { get; set; }
        public string Account { get; set; }
        public DateTime TS { get; set; }
        public List<Fill> Fills { get; set; }
        public int TotalFilled { get; set; }
        public bool Open { get; set; }
    }
}
