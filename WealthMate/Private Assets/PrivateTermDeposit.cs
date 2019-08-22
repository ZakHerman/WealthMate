using System;

public class PrivateTermDeposit
{
    private float amountInvested { get; set; }
    private float interestDecision { get; set; }
    TermDeposit pTermDeposit;

    public PrivateTermDeposit(float amountInvested, float interestDecision, TermDeposit pTermDeposit)
	{
        this.amountInvested = amountInvested;

	}

    public void OwnedTermDeposit(TermDeposit pTermDeposit, float amountInvested, float interestDecision)
    {
        pTermDeposit = new TermDeposit();
    }

    public override string ToString()
    {
        return base.ToString();
    }
}
