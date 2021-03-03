using ChannelEngine.Core;
using ChannelEngine.Core.Clients;
using ChannelEngine.Core.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChannelEngine.Console
{
    public class RetreiveOrders : BackgroundService
    {
        private readonly IOrderClient _orderClient;
        private readonly ILogger<RetreiveOrders> _logger;
        private readonly IHost _host;
        private readonly IAssessmentLogic _assessment;
        public RetreiveOrders(IHost host, IOrderClient client, IAssessmentLogic assessment, ILogger<RetreiveOrders> logger)
        {
            _orderClient = client;
            _logger = logger;
            _host = host;
            _assessment = assessment;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            IEnumerable<TopResultView> top5 = await _assessment.RetreiveTop(5);
            System.Console.WriteLine($"Top 5 results");
            foreach (TopResultView resultView in top5)
            {
                if(resultView.ProductName == "001201-M")
                {
                    StockItem item = await _assessment.GetStock(resultView.ProductName);
                    _logger.LogDebug($"Current stock: {item.MerchantProductNo}-{item.StockLocationId} is {item.Stock}");
                    item.Stock = 25;
                    await _assessment.SetStock(item);

                    item = await _assessment.GetStock(resultView.ProductName);
                    _logger.LogDebug($"New stock: {item.MerchantProductNo}-{item.StockLocationId} is {item.Stock}");
                }
                System.Console.WriteLine($"{resultView.Gtin}, {resultView.ProductName}, {resultView.Quantity}");
            }

            await _host.StopAsync();
        }
    }
}
