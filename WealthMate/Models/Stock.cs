using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace WealthMate.Models
{
    public class Stock : INotifyPropertyChanged
    {
        private float _currentPrice;
        private bool _positiveDayReturns;
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

        [JsonProperty("price_close")]
        public float PriceClose { get; set; }

        [JsonProperty("day_high")]
        public float DayHigh { get; set; }

        [JsonProperty("day_low")]
        public float DayLow { get; set; }

        [JsonProperty("52_week_high")]
        public float FiftyTwoWeekHigh { get; set; }

        [JsonProperty("52_week_low")]
        public float FiftyTwoWeekLow { get; set; }

        public float DayAverage { get; set; }

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

        public bool PositiveDayReturns
        {
            get => _positiveDayReturns;
            set
            {
                if (_currentPrice >= PriceClose)
                    _positiveDayReturns = true;
                else
                    _positiveDayReturns = false;
            }
        }
        private float _dayReturn;
        public float DayReturn
        {
            get => _dayReturn;
            set => _dayReturn = _currentPrice - PriceClose;
        }

        private float _dayReturnRate;
        public float DayReturnRate
        {
            get => _dayReturnRate;
            set => _dayReturnRate = (_dayReturn / PriceClose) * 100;
        }
        public Stock()
        {
        }

        public Stock(string name, float price, DateTime priceDate, int shares, int volume)
        {
            this.CurrentPrice = price;
            this.LastTrade = priceDate;
            this.Shares = shares;
            this.Volume = volume;
            this.CompanyName = name;

            //defaults:
            this.PriceOpen = price;
            this.PriceClose = price;
            this.DayHigh = price;
            this.DayLow = price;
            this.DayAverage = price;
        }

        public Stock(string companyName)
        {
            CompanyName = companyName;
            //defaults:
            PriceOpen = 0.00f;
            PriceClose = 0.00f;
            CurrentPrice = 0.00f;
            DayAverage = 0.00f;
            DayHigh = 0.00f;
            DayLow = 0.00f;
            FiftyTwoWeekHigh = 0.00f;
            FiftyTwoWeekLow = 0.00f;
            Shares = 0;
            Volume = 0;

        }

        //Need to add Set methods for updating variables directly from the database when needed, e.g. public void refresh() {}
        public void UpdateStock()
        {
            //.....
            PositiveDayReturns = true;
            DayReturn = 0.0f;
            DayReturnRate = 0.0f;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
