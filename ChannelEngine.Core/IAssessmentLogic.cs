using ChannelEngine.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChannelEngine.Core
{
    public interface IAssessmentLogic
    {
        Task<StockItem> GetStock(string productCode);
        Task<IEnumerable<TopResultView>> RetreiveTop(int number);
        Task SetStock(StockItem item);
    }
}