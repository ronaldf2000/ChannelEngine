using ChannelEngine.Core.Models;
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

    public class OrderClient : IOrderClient
    {
        private readonly HttpClient _client;
        private readonly string _apiKey;
        private readonly ILogger<OrderClient> _logger;
        public OrderClient(HttpClient client, IConfiguration configuration, ILogger<OrderClient> logger)
        {
            _client = client;
            _apiKey = configuration["apiKey"];
            _logger = logger;
            if (string.IsNullOrEmpty(_apiKey))
            {
                _logger.LogWarning("Error no api key in configuration using default dev api key");
                _apiKey = "541b989ef78ccb1bad630ea5b85c6ebff9ca3322";
            }
        }

        //for assesment no need to implement other filters
        public async Task<OrderResponse> RetrieveOrders(int page, OrderStatus? status = null)
        {
            //https://api-dev.channelengine.net/api/v2/orders/?apikey=541b989ef78ccb1bad630ea5b85c6ebff9ca3322&status=IN_PROGRESS
            string url = $"?apikey={_apiKey}";
            if (status != null)
            {
                url += $"&status={status}";
            }
            return await _client.GetFromJsonAsync<OrderResponse>(url);
        }
    }
}
