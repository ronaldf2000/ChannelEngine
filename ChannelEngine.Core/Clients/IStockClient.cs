using ChannelEngine.Core.Models;
using System.Threading.Tasks;

namespace ChannelEngine.Core.Clients
{
    public interface IStockClient
    {
        Task<ChannelResponse<StockItem>> GetStock(string productCode, int stockLocation);
        Task<ChannelResponse<StockLocation>> GetStockLocations();
        Task SetStock(StockItem item);
    }
}