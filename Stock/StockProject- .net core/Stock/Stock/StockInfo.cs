

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WebApplication2.Models
{
    public class StockInfo
    {
        [JsonProperty("meta")]
        public Dictionary<string, string> Meta { get; set; }

        [JsonProperty("values")]
        public List<TwelveDataValue> Values { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        public StockInfo()
        {
            Values = new List<TwelveDataValue>();
            Meta = new Dictionary<string, string>();
        }
    }

    public class TwelveDataValue
    {
        [JsonProperty("datetime")]
        public DateTime DateTime { get; set; }


        [JsonProperty("close")]
        public decimal Close { get; set; }

        
    }
}
