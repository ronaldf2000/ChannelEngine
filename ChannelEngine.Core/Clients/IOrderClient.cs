using ChannelEngine.Core.Models;
using System.Threading.Tasks;

namespace ChannelEngine.Core.Clients
{
    public interface IOrderClient
    {
        Task<OrderResponse> RetrieveOrders(int page, OrderStatus? status = null);
    }
}