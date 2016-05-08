using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockfighterClient.Responses
{
    public class BaseResponse
    {
        public bool Ok { get; set; }
        public string Error { get; set; }
    }
}
