using ChannelEngine.Core.Models;
using System.Threading.Tasks;

namespace ChannelEngine.Core.Clients
{
    public interface IOrderClient
    {
        Task<ChannelResponse<Order>> RetrieveOrders(int page, OrderStatus? status = null);
    }
}