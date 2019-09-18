using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WealthMate.Models;

namespace WealthMate.Services
{
    public static class DataService
    {
        public static readonly HttpClient Client = new HttpClient();
        public const string BaseUrl = "https://wealthmate.azurewebsites.net/api/";

        public static ObservableCollection<Stock> Stocks { get; private set; }
        public static ObservableCollection<TermDeposit> TermDeposits { get; private set; }

        public static async Task FetchStocksAsync()
        {
            var res = await Client.GetAsync(BaseUrl + "getstocksnzx");
            var content = await res.Content.ReadAsStringAsync();
            Stocks = JsonConvert.DeserializeObject<ObservableCollection<Stock>>(content);
        }

        public static async Task<Stock> GetStockAsync(string symbol)
        {
            return await Task.FromResult(Stocks.FirstOrDefault(s => s.Symbol == symbol));
        }

        public static async Task FetchTermDepositsAsync()
        {
            var res = await Client.GetAsync(BaseUrl + "gettermdeposits");
            var content = await res.Content.ReadAsStringAsync();
            TermDeposits = JsonConvert.DeserializeObject<ObservableCollection<TermDeposit>>(content);
        }
    }
}
