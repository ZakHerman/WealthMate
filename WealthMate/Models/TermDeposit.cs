using Newtonsoft.Json;

namespace WealthMate.Models
{
    public class TermDeposit
    {
        [JsonProperty("provider")]
        public string Provider { get; set; }

        [JsonProperty("minimumDeposit")]
        public int? MinDeposit { get; set; }

        [JsonProperty("maximumDeposit")]
        public int? MaxDeposit { get; set; }

        [JsonProperty("term")]
        public int LengthInMonths { get; set; }

        [JsonProperty("rate")]
        public float InterestRate { get; set; }

        public string Logo { get; set; }

        public bool NoMaxDeposit; 
        public bool NoMinimumDeposit;

        public TermDeposit()
        {
        }
        
        public TermDeposit(string provider, int? minDeposit, int? maxDeposit, int length, float interestRate)
        {
            Provider = provider;
            MinDeposit = minDeposit;
            MaxDeposit = maxDeposit;
            LengthInMonths = length;
            InterestRate = interestRate;
            if(MinDeposit == 0)
            {
                NoMinimumDeposit = true;
            }
            else
            {
                NoMinimumDeposit = false;
            }
            if (MaxDeposit == 0 || MaxDeposit == null)
            {
                NoMaxDeposit = true;
            }
            else
            {
                NoMaxDeposit = false;
            }
        }
    }
}