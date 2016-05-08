using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockfighterClient.Responses
{
    public class QuoteResponse : BaseResponse
    {
        public string Symbol { get; set; }
        public string Venue { get; set; }
        public int Bid { get; set; }
        public int Ask { get; set; }
        public int BidSize { get; set; }
        public int AskSize { get; set; }
        public int BidDepth { get; set; }
        public int AskDepth { get; set; }
        public int Last { get; set; }
        public int LastSize { get; set; }
        public DateTime LastTrade { get; set; }
        public DateTime QuoteTime { get; set; }
    }
}
