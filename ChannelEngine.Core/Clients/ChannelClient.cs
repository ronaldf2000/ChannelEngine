using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text;
using System.Web;

namespace ChannelEngine.Core.Clients
{
    public class ChannelClient
    {
        protected readonly HttpClient _client;
        private string _apiKey;

        public ChannelClient(HttpClient client, IConfiguration configuration, ILogger logger)
        {
            _client = client;

            _apiKey = configuration["apiKey"];
            if (string.IsNullOrEmpty(_apiKey))
            {
                logger.LogWarning("Error no api key in configuration using default dev api key");
                _apiKey = "541b989ef78ccb1bad630ea5b85c6ebff9ca3322";
            }

        }

        protected Dictionary<string, string> GetQueryParams()
        {
            Dictionary<string, string> query = new Dictionary<string, string>();
            query["apikey"] = _apiKey;
            return query;
        }
    }
}
