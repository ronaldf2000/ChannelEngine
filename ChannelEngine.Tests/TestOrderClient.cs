using ChannelEngine.Core.Clients;
using ChannelEngine.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngine.Tests
{
    public class TestOrderClient : IOrderClient
    {
        public async Task<OrderResponse> RetrieveOrders(int page, OrderStatus? status = null)
        {
            string testData = await File.ReadAllTextAsync("TestData.json");
            return JsonConvert.DeserializeObject<OrderResponse>(testData);
        }
    }
}
