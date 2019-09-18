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

        public float NoMaxDeposit; 
        public float NoMinimumDeposit;

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
            NoMaxDeposit = 0;
            NoMinimumDeposit = 0;
        }
    }
}