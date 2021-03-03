using ChannelEngine.Core.Clients;
using ChannelEngine.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngine.Core
{
    public class AssessmentLogic : IAssessmentLogic
    {
        private readonly IOrderClient _orderClient;
        private readonly ILogger<AssessmentLogic> _logger;

        public AssessmentLogic(IOrderClient client, ILogger<AssessmentLogic> logger)
        {
            _orderClient = client;
            _logger = logger;
        }

        private async Task<List<Line>> RetreiveLines()
        {
            //Cancellation should be implemented in real world apps
            //to so see if we can start a discussion
            //var response = _orderClient.RetrieveOrders(1, OrderStatus.IN_PROGRESS);
            int page = 1;
            List<Line> lines = new List<Line>();
            OrderResponse response;
            try
            {
                do
                {
                    _logger.LogDebug($"Retreiving page {page}");
                    response = await _orderClient.RetrieveOrders(page, status: OrderStatus.IN_PROGRESS);
                    if (response == null)
                    {
                        _logger.LogWarning("RetreiveOrders returned null which was not really expected");
                    }
                    else
                    {
                        if (page == 1)
                        {
                            _logger.LogDebug($"Total items {response.TotalCount} pages {response.TotalCount / response.ItemsPerPage + 1}");
                        }
                        foreach (Content content in response.Content)
                        {
                            lines.AddRange(content.Lines);
                        }
                    }
                    page++;
                } while (response != null && page < response.TotalCount / response.ItemsPerPage);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while retreiving data: {ex.Message}");
                _logger.LogDebug(ex.ToString());
            }
            return lines;
        }

        public async Task<IEnumerable<TopResultView>> RetreiveTop(int top)
        {
            List<Line> lines = await RetreiveLines();
            //Assumption here is MerchantProductNo is product name 
            return lines.GroupBy(l => l.MerchantProductNo)
                .Select(g => new TopResultView()
                {
                    Gtin = g.First().Gtin,
                    ProductName = g.Key,
                    Quantity = g.Sum(l => l.Quantity)
                })
                .OrderByDescending(rv => rv.Quantity)
                .Take(top);
        }
    }
}

