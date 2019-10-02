using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WealthMate.Services;

namespace WealthMate.ViewModels
{
    public class StockViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private float _currentPrice;
        private DateTime _lastTrade;

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

        public string Symbol { get; set; }
        public string CompanyName { get; set; }
        public float PriceOpen { get; set; }
        public float PriceClose { get; set; }
        public float DayHigh { get; set; }
        public float DayLow { get; set; }
        public float FiftyTwoWeekHigh { get; set; }
        public float FiftyTwoWeekLow { get; set; }
        public long Shares { get; set; }
        public int Volume { get; set; }

        public DateTime LastTrade
        {
            get => _lastTrade;
            set => _lastTrade = value.ToLocalTime();
        }

        public float DayReturn { get; set; }
        public float DayReturnRate { get; set; }
        public bool PositiveDayReturns { get; set; }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
