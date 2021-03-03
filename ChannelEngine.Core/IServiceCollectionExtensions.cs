using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using ChannelEngine.Core.Clients;

namespace ChannelEngine.Core
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, string baseUrl)
        {
            services.AddHttpClient<IOrderClient, OrderClient>(cb => cb.BaseAddress = new Uri(baseUrl + "orders/"));
            services.AddHttpClient<IStockClient, StockClient>(cb => cb.BaseAddress = new Uri(baseUrl));
            services.AddScoped<IAssessmentLogic, AssessmentLogic>();
            return services;
        }
    }
}
