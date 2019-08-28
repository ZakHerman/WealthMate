using System;

//Object created, not instantiated yet.

namespace WealthMate.PublicAssets
{
    public class Stock
    {
        //Data to receive directly from NZX (Entity Relationship diagram)
        public float CurrentPrice { get; set; }
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

        public Stock(float price, DateTime priceDate, int shares, int volume)
        {
            CurrentPrice = price;
            PriceDate = priceDate;
            Shares = shares;
            Volume = volume;

            //defaults:
            PriceOpen = price;
            PriceClose = price;
            DayHigh = price;
            DayLow = price;
            DayAverage = price;
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
    }
}
