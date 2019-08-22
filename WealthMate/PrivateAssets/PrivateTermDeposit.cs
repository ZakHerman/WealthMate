namespace WealthMate.PrivateAssets
{
    public class PrivateTermDeposit
    {
        public float AmountInvested { get; set; }
        public float InterestDecision { get; set; }

        public TermDeposit PTermDeposit { get; set; }
        //TermDeposit pTermDeposit;

        public PrivateTermDeposit(float amountInvested, float interestDecision, TermDeposit pTermDeposit)
        {
            AmountInvested = amountInvested;
            InterestDecision = interestDecision;
            PTermDeposit = pTermDeposit;
        }

        public void OwnedTermDeposit(TermDeposit pTermDeposit, float amountInvested, float interestDecision)
        {
            //pTermDeposit = new TermDeposit();
        }

        /*public override string ToString()
        {
            return base.ToString();
        }*/
    }
}
