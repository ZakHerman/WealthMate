using System;

public class TermDeposit
{
    private string provider { get; set; }
    private float minDeposit { get; set; }
    private float maxDeposit { get; set; }
    private float length { get; set; }
    private float interestRate { get; set; }

    public TermDeposit(string provider, float minDeposit, float maxDeposit, float length, float interestRate)
	{
        this.provider = provider;
        this.minDeposit = minDeposit;
        this.maxDeposit = maxDeposit;
        this.length = length;
        this.interestRate = interestRate;
	}

    // TODO: hard code the term deposit info from each bank
}
