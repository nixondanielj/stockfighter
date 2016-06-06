using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockfighterEngine
{
    public interface IStrategy
    {
        Task<bool> IsComplete(Agent agent);
        Task<bool> IsCancelled(Agent agent);
        Task Execute(Agent agent);
    }
}
