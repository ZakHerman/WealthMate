using System;
using Newtonsoft.Json;
using SQLite;

namespace WealthMate.Models
{
    public class StockHistory
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // Reference to the stock using its symbol
        [Indexed(Name = "StockHistoryPrimaryKey", Order = 1, Unique = true)]
        public string Symbol { get; set; }

        // Reference date for given stock
        [Indexed(Name = "StockHistoryPrimaryKey", Order = 2, Unique = true)]
        public DateTime Date { get; set; }

        // Opening market price
        [JsonProperty("open")]
        public float PriceOpen { get; set; }

        // Closing market price
        [JsonProperty("close")]
        public float PriceClose { get; set; }

        // Highest price for the day during trading hours
        [JsonProperty("high")]
        public float DayHigh { get; set; }

        // Lowest price for the day during trading hours
        [JsonProperty("low")]
        public float DayLow { get; set; }
    }
}
