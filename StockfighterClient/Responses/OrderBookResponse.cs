using StockfighterClient.Models;
using System;
using System.Collections.Generic;

namespace StockfighterClient.Responses
{
    public class OrderBookResponse : BaseResponse
    {
        public string Venue { get; set; }
        public string Symbol { get; set; }
        public List<Order> Bids { get; set; }
        public List<Order> Asks { get; set; }
        public DateTime TS { get; set; }
    }
}