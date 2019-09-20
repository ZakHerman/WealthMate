using System;
using Newtonsoft.Json;

namespace WealthMate.Models
{
    public class StockHistory                               //For price gragh on stock details page
    {
        public DateTime Date { get; set; }                  //Captures date of price

        [JsonProperty("open")]
        public float PriceOpen { get; set; }                //Price open of given date

        [JsonProperty("close")]
        public float PriceClose { get; set; }               //Price close of given date

        [JsonProperty("high")]
        public float DayHigh { get; set; }                  //Price high of given date

        [JsonProperty("low")]
        public float DayLow { get; set; }               //Price low of given date

                                                        //Note: Can do candle stick graph using these values
    }
}
