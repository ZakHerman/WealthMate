using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using SQLite;

namespace WealthMate.Models
{
    public class Stock : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private float _currentPrice;

        [JsonProperty("price")]
        public float CurrentPrice
        {
            get => _currentPrice;
            set
            {
                if (_currentPrice == value)
                    return;

                _currentPrice = value;
                OnPropertyChanged();
            }
        }

        // NZX stock exchange symbol
        [JsonProperty("symbol"), PrimaryKey]
        public string Symbol { get; set; }

        [JsonProperty("name")]
        public string CompanyName { get; set; }

        // Price stock opened the market day with
        [JsonProperty("price_open")]
        public float PriceOpen { get; set; }

        // Price stock closed the previous market day with
        [JsonProperty("close_yesterday")]
        public float PriceClose { get; set; }

        // Highest price during market day
        [JsonProperty("day_high")]
        public float DayHigh { get; set; }

        // Lowest price during market day
        [JsonProperty("day_low")]
        public float DayLow { get; set; }

        // Highest price in the past 52 weeks
        [JsonProperty("52_week_high")]
        public float FiftyTwoWeekHigh { get; set; }

        // Lowest price in the past 52 weeks
        [JsonProperty("52_week_low")]
        public float FiftyTwoWeekLow { get; set; }

        // Amount of shares available to purchase
        [JsonProperty("shares")]
        public long Shares { get; set; }

        // Volume of shares traded during market day
        [JsonProperty("volume")]
        public int Volume { get; set; }

        [JsonProperty("last_trade_time")]
        public DateTime LastTrade { get; set; }

        [Ignore]
        public float DayReturn { get; set; }

        [Ignore]
        public float DayReturnRate { get; set; }

        //Flag indicators for view trigger purposes
        [Ignore]
        public bool PositiveDayReturns { get; set; }
        
        [Ignore]
        public bool NoDayReturns { get; set; }

        // Updates stock to ensure values are being calculated and displayed correctly when user views
        public void UpdateStock()
        {
            PositiveDayReturns = CurrentPrice > PriceOpen;
            NoDayReturns = CurrentPrice == PriceOpen;
            DayReturn = CurrentPrice - PriceOpen;

            if (PriceOpen == 0)
                DayReturnRate = 0.0f;
            else
                DayReturnRate = DayReturn / PriceOpen * 100;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
