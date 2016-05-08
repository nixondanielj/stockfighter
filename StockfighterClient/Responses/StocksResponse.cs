using StockfighterClient.Models;
using System.Collections.Generic;

namespace StockfighterClient.Responses
{
    public class StocksResponse : BaseResponse
    {
        public List<Stock> Symbols { get; set; }
    }
}