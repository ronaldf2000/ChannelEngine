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
    public class StockClient : ChannelClient, IStockClient
    {
        private readonly ILogger<OrderClient> _logger;
        public StockClient(HttpClient client, IConfiguration configuration, ILogger<OrderClient> logger) : base(client, configuration, logger)
        {
            _logger = logger;
        }

        public async Task<ChannelResponse<StockLocation>> GetStockLocations()
        {
            var query = GetQueryParams();
            return await _client.GetFromJsonAsync<ChannelResponse<StockLocation>>(QueryHelpers.AddQueryString("stocklocations/", query));
        }
        public async Task<ChannelResponse<StockItem>> GetStock(string productCode, int stockLocation)
        {
            var query = GetQueryParams();
            query["stockLocationIds"] = stockLocation.ToString();
            query["skus"] = productCode;
            return await _client.GetFromJsonAsync<ChannelResponse<StockItem>>(QueryHelpers.AddQueryString("offer/stock/", query));
        }
        public async Task SetStock(StockItem item)
        {
            var query = GetQueryParams();
            await _client.PutAsJsonAsync(QueryHelpers.AddQueryString("offer/stock/", query), item);
        }
    }
}
