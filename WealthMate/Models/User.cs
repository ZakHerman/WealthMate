using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace WealthMate.Models
{
    public class User
    {
        public Portfolio Portfolio { get; set; }                                    //Each user has their own customized portfolio
        public ObservableCollection<Stock> WatchListStocks { get; set; }            //Each user has their own customized watchlist

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("verified_email")]
        public bool VerifiedEmail { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("given_name")]
        public string GivenName { get; set; }

        [JsonProperty("family_name")]
        public string FamilyName { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("picture")]
        public string Picture { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        public User()
        {
            Portfolio = new Portfolio();
            WatchListStocks = new ObservableCollection<Stock>();
        }
    }
}
