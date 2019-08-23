using System;

//Object created, not instantiated yet.

public class Stock
{
    //Data to receive directly from NZX (Entity Relationship diagram)
    private float current_price {get; set;}
    private float price_open {get; set;}
    private float price_close {get; set;}
    private float day_high {get; set;}
    private float day_low {get; set;}
    private float year_high {get; set;}
    private float year_low {get; set;}
    private float day_average {get; set;}
    private int shares {get; set;}
    private int volume {get; set;}
    private string company_name {get;}

    public Stock(float price, DateTime price_date, int shares, int volume)
	{
        this.current_price = price;
        this.shares = shares;
        this.volume = volume;

        //defaults:
        this.price_open = price;
        this.price_close = price;
        this.day_high = price;
        this.day_low = price;
        this.day_average = price;
	}

    public Stock(string company_name)
    {
        this.company_name = company_name;
        //defaults:
        this.price_open = 0.00f;
        this.price_close = 0.00f;
        this.current_price = 0.00f;
        this.day_average = 0.00f;
        this.day_high = 0.00f;
        this.day_low = 0.00f;
        this.year_high = 0.00f;
        this.year_low = 0.00f;
        this.shares = 0;
        this.volume = 0;

    }

    //Need to add Set methods for updating variables directly from the database when needed, e.g. public void refresh() {}
    public void updateStock()
    {
        //.....
    }

}

