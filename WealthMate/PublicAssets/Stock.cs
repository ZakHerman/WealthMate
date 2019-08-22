using System;

//Object created, not instantiated yet.

public class Stock
{
    //Data to receive directly from NZX (Entity Relationship diagram)
    public float CurrentPrice;
    public float PriceOpen;
    public float PriceClose;
    public float DayHigh;
    public float DayLow;
    public float YearHigh;
    public float YearLow;
    public float DayAverage;
    public int Shares;
    public int Volume;
    public string CompanyName;

    public Stock(float price, DateTime price_date, int shares, int volume)
	{
        this.CurrentPrice = price;
        this.Shares = shares;
        this.Volume = volume;

        //defaults:
        this.PriceOpen = price;
        this.PriceClose = price;
        this.DayHigh = price;
        this.DayLow = price;
        this.DayAverage = price;
	}

    public Stock(string company_name)
    {
        this.CompanyName = company_name;
        //defaults:
        this.PriceOpen = 0.00f;
        this.PriceClose = 0.00f;
        this.CurrentPrice = 0.00f;
        this.DayAverage = 0.00f;
        this.DayHigh = 0.00f;
        this.DayLow = 0.00f;
        this.YearHigh = 0.00f;
        this.YearLow = 0.00f;
        this.Shares = 0;
        this.Volume = 0;

    }

    //Need to add Set methods for updating variables directly from the database when needed, e.g. public void refresh() {}
    public void UpdateStock()
    {
        //.....
    }
}

