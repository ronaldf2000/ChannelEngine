using ChannelEngine.Core.Models;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngine.Core.Clients
{
    public enum OrderStatus
    {
        IN_PROGRESS,
        SHIPPED,
        IN_BACKORDER,
        MANCO,
        CANCELED,
        IN_COMBI,
        CLOSED,
        NEW,
        RETURNED,
        REQUIRES_CORRECTION
    }

    public class OrderClient : ChannelClient, IOrderClient
    {
        private readonly ILogger<OrderClient> _logger;
        public OrderClient(HttpClient client, IConfiguration configuration, ILogger<OrderClient> logger) : base(client, configuration, logger)
        {
            _logger = logger;
        }

        //for assesment no need to implement other filters
        public async Task<ChannelResponse<Order>> RetrieveOrders(int page, OrderStatus? status = null)
        {
            var query = GetQueryParams();
            if (status != null)
            {
                query["status"] = status.ToString();
            }
            return await _client.GetFromJsonAsync<ChannelResponse<Order>>(QueryHelpers.AddQueryString("", query));
        }
    }
}
