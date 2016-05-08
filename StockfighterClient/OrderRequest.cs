using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockfighterClient
{
    public class OrderRequest
    {
        public string Account { get; set; }
        public string Venue { get; set; }
        public string Stock { get; set; }
        public int Price { get; set; }
        public int Qty { get; set; }
        [JsonIgnore]
        public bool IsBuy
        {
            get
            {
                return Direction == "buy";
            }
            set
            {
                Direction = "sell";
                if (value)
                {
                    Direction = "buy";
                }
            }
        }
        [JsonIgnore]
        public OrderTypes Type
        {
            get
            {
                switch (OrderType)
                {
                    case "limit":
                        return OrderTypes.Limit;
                    case "market":
                        return OrderTypes.Market;
                    case "fill-or-kill":
                        return OrderTypes.FOK;
                    case "immediate-or-cancel":
                        return OrderTypes.IOC;
                    default:
                        return OrderTypes.Limit;
                }
            }
            set
            {
                switch (value)
                {
                    case OrderTypes.Limit:
                        OrderType = "limit";
                        break;
                    case OrderTypes.Market:
                        OrderType = "market";
                        break;
                    case OrderTypes.FOK:
                        OrderType = "fill-or-kill";
                        break;
                    case OrderTypes.IOC:
                        OrderType = "immediate-or-cancel";
                        break;
                    default:
                        OrderType = "limit";
                        break;
                }
            }
        }

        /// <summary>
        /// Set with IsBuy
        /// </summary>
        public string Direction { get; private set; }
        /// <summary>
        /// Set with Type
        /// </summary>
        public string OrderType { get; private set; }
    }
    public enum OrderTypes
    {
        Limit,
        Market,
        FOK,
        IOC
    }
}
