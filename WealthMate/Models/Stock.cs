using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WealthMate.Models
{
    public class Stock : INotifyPropertyChanged
    {
        private float _currentPrice;
        //Data to receive directly from NZX (Entity Relationship diagram)
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
        public DateTime PriceDate { get; set; }
        public float PriceOpen { get; set; }
        public float PriceClose { get; set; }
        public float DayHigh { get; set; }
        public float DayLow { get; set; }
        public float FiftyTwoWeekHigh { get; set; }
        public float FiftyTwoWeekLow { get; set; }
        public float DayAverage { get; set; }
        public int Shares { get; set; }
        public int Volume { get; set; }
        public string CompanyName { get; set; }
        public string Symbol { get; set; }

        public Stock()
        {

        }

        public Stock(string name, float price, DateTime priceDate, int shares, int volume)
        {
            this.CurrentPrice = price;
            this.PriceDate = priceDate;
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
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
