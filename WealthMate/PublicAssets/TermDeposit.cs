namespace WealthMate.PublicAssets
{
    public class TermDeposit
    {
        public string Provider { get; set; }
        public float MinDeposit { get; set; }
        public float MaxDeposit { get; set; }
        public float Length { get; set; }
        public float InterestRate { get; set; }

        public TermDeposit(string provider, float minDeposit, float maxDeposit, float length, float interestRate)
        {
            Provider = provider;
            MinDeposit = minDeposit;
            MaxDeposit = maxDeposit;
            Length = length;
            InterestRate = interestRate;
        }

        // TODO: hard code the term deposit info from each bank
    }
}
