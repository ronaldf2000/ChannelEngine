using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelEngine.Core.Models
{
    public class StockItem
    {
        public string MerchantProductNo { get; set; }
        public int StockLocationId { get; set; }
        public int Stock { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
