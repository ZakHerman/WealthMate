namespace WealthMate.ViewModels
{
    public class TermDepositViewModel
    {
        public string Provider { get; set; }
        public int? MinDeposit { get; set; }
        public int? MaxDeposit { get; set; }
        public int LengthInMonths { get; set; }
        public float InterestRate { get; set; }
        public string Logo { get; set; }
    }
}
