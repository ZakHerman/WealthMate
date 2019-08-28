using System;

public class SavingsAccount
{
    private float balance;
    private float interest_rate;
    private int min_deposit;
    private int max_balance;

    private string company_name;
    private string account_type;

    public SavingsAccount(string company_name, string account_type,float rate,int min)
    {
        this.company_name = company_name;
        this.account_type = account_type;
        this.balance = 0.00f;
        this.interest_rate = rate;
        this.min_deposit = min;
        this.max_balance = 0;
    }

    //default constructor
    public SavingsAccount(string company_name,string account_type)
	{
        this.company_name = company_name;
        this.account_type = account_type;
        this.balance = 0.00f;
        this.interest_rate = 0.00f;
        this.min_deposit = 0;
        this.max_balance = 0;
	}

    public string getName()
    {
        return this.company_name;
    }

    public string getType()
    {
        return this.account_type;
    }

    public float getBalance()
    {
        return this.balance;
    }

    public float getRate()
    {
        return this.interest_rate;
    }

    public int getMin()
    {
        return this.min_deposit;
    }

    public int getMax()
    {
        return this.max_balance;
    }
}
