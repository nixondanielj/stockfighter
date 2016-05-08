using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockfighterClient.Models
{
    public class Order
    {
        public long Price { get; set; }
        public long Qty { get; set; }
        public bool IsBuy { get; set; }
    }
}
