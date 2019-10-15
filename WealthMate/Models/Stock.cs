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

        [JsonProperty("symbol"), PrimaryKey]                    //Stocks NZX symbol
        public string Symbol { get; set; }

        [JsonProperty("name")]
        public string CompanyName { get; set; }

        [JsonProperty("price_open")]                //Price stock opened the market day with
        public float PriceOpen { get; set; }

        [JsonProperty("close_yesterday")]       //Price stock closed the previous market day with
        public float PriceClose { get; set; }

        [JsonProperty("day_high")]                  //Highest price during market day
        public float DayHigh { get; set; }

        [JsonProperty("day_low")]                   //Lowest price during market day
        public float DayLow { get; set; }

        [JsonProperty("52_week_high")]                  //Highest price in the past 52 weeks
        public float FiftyTwoWeekHigh { get; set; }

        [JsonProperty("52_week_low")]                   //Lowest price in the past 52 weeks
        public float FiftyTwoWeekLow { get; set; }

        [JsonProperty("shares")]                        //Amount of shares available to purchase
        public long Shares { get; set; }

        [JsonProperty("volume")]                        //Volume of shares traded during market day
        public int Volume { get; set; }

        [JsonProperty("last_trade_time")]
        public DateTime LastTrade { get; set; }

        [Ignore]
        public float DayReturn { get; set; }

        [Ignore]
        public float DayReturnRate { get; set; }

        [Ignore]
        public bool PositiveDayReturns { get; set; }                        //Flag indicators for view trigger purposes
        
        [Ignore]
        public bool NoDayReturns { get; set; }

        //Need to add Set methods for updating variables directly from the database when needed, e.g. public void refresh() {}
        public void UpdateStock()                               //updates stock to ensure values are being calculated and displayed correctly when user views
        {
            PositiveDayReturns = CurrentPrice > PriceOpen;
            NoDayReturns = CurrentPrice == PriceOpen;
            DayReturn = CurrentPrice - PriceOpen;
            DayReturnRate = DayReturn / PriceOpen * 100;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
