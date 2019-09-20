using System;
using Newtonsoft.Json;

namespace WealthMate.Models
{
    public class StockHistory
    {
        public DateTime Date { get; set; }

        [JsonProperty("open")]
        public float PriceOpen { get; set; }

        [JsonProperty("close")]
        public float PriceClose { get; set; }

        [JsonProperty("high")]
        public float DayHigh { get; set; }

        [JsonProperty("low")]
        public float DayLow { get; set; }
    }
}
