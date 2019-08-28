namespace WealthMate.PublicAssets
{
    public class SavingsAccount
    {
        public float Balance { get; set; }
        public float InterestRate { get; set; }
        public int MinDeposit { get; set; }
        public int MaxBalance { get; set; }
        public string CompanyName { get; set; }
        public string AccountType { get; set; }

        // Default constructor
        public SavingsAccount()
        {

        }

        public SavingsAccount(string companyName, string accountType)
        {
            CompanyName = companyName;
            AccountType = accountType;
            Balance = 0.00f;
            InterestRate = 0.00f;
            MinDeposit = 0;
            MaxBalance = 0;
        }

        public SavingsAccount(string companyName, string accountType, float rate, int min)
        {
            CompanyName = companyName;
            AccountType = accountType;
            Balance = 0.00f;
            InterestRate = rate;
            MinDeposit = min;
            MaxBalance = 0;
        }
    }
}
