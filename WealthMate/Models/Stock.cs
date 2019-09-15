using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace WealthMate.Models
{
    public class Stock : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private float _currentPrice;
        private DateTime _lastTrade;

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

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("name")]
        public string CompanyName { get; set; }

        [JsonProperty("price_open")]
        public float PriceOpen { get; set; }

        [JsonProperty("close_yesterday")]
        public float PriceClose { get; set; }

        [JsonProperty("day_high")]
        public float DayHigh { get; set; }

        [JsonProperty("day_low")]
        public float DayLow { get; set; }

        [JsonProperty("52_week_high")]
        public float FiftyTwoWeekHigh { get; set; }

        [JsonProperty("52_week_low")]
        public float FiftyTwoWeekLow { get; set; }

        [JsonProperty("shares")]
        public long Shares { get; set; }

        [JsonProperty("volume")]
        public int Volume { get; set; }

        [JsonProperty("last_trade_time")]
        public DateTime LastTrade
        {
            get => _lastTrade;
            set => _lastTrade = value.ToLocalTime();
        }

        public float DayReturn { get; set; }
        public float DayReturnRate { get; set; }
        public bool PositiveDayReturns { get; set; }

        //Need to add Set methods for updating variables directly from the database when needed, e.g. public void refresh() {}
        public void UpdateStock()
        {
            PositiveDayReturns = CurrentPrice >= PriceOpen;
            DayReturn = CurrentPrice - PriceOpen;
            DayReturnRate = DayReturn / PriceOpen * 100;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
