using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockfighterClient.Models
{
    public class Fill
    {
        public int Price { get; set; }
        public int Qty { get; set; }
        public DateTime TS { get; set; }
    }
}
