using System;

//Object created, not instantiated yet.

public class Class1
{
    //Data to receive directly from NZX (Entity Relationship diagram)
    private float current_price;
    private float price_open;
    private float price_close;
    private float day_high;
    private float day_low;
    private float year_high;
    private float year_low;
    private float day_average;
    private int shares;
    private int volume;
    private string company_name;

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


    //Getters:
    public float getPrice()
    {
        return this.current_price;
    }

    public float getOpenPrice()
    {
        return this.price_open;
    }

    public float getClosePrice()
    {
        return this.price_close;
    }

    public float getDayHigh()
    {
        return this.day_high;
    }

    public float getDayLow()
    {
        return this.day_low;
    }

    public float getYearHigh()
    {
        return this.year_high;
    }

    public float getYearLow()
    {
        return this.year_low;
    }

    public int getShares()
    {
        return this.shares;
    }

    public int getVolume()
    {
        return this.volume;
    }

}

