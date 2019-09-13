using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WealthMate.Models;

namespace WealthMate.Services
{
    public static class Api
    {
        public static readonly HttpClient Client = new HttpClient();
        public const string BaseUrl = "https://wealthmate.azurewebsites.net/api/";

        public static ObservableCollection<Stock> Stocks { get; set; }

        public static async Task<ObservableCollection<Stock>> GetStocksAsync()
        {
            var res = await Client.GetAsync(BaseUrl + "GetStocksNzx");
            var content = await res.Content.ReadAsStringAsync();
            var stocks = JsonConvert.DeserializeObject<ObservableCollection<Stock>>(content);

            return stocks;
        }
    }
}
