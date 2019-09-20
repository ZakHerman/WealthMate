using System.Collections.ObjectModel;
using System.Diagnostics;
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
        public static ObservableCollection<StockHistory> StockHistory { get; private set; }

        // Get request to stocks data webservice
        public static async Task FetchStocksAsync()
        {
            var res = await Client.GetAsync(BaseUrl + "getstocksnzx");
            var content = await res.Content.ReadAsStringAsync();
            Stocks = JsonConvert.DeserializeObject<ObservableCollection<Stock>>(content);
        }

        // Get specific stock from stocks list in memory
        public static async Task<Stock> GetStockAsync(string symbol)
        {
            return await Task.FromResult(Stocks.FirstOrDefault(s => s.Symbol == symbol));
        }

        // Get request to term deposits data webservice
        public static async Task FetchTermDepositsAsync()
        {
            var res = await Client.GetAsync(BaseUrl + "gettermdeposits");
            var content = await res.Content.ReadAsStringAsync();
            TermDeposits = JsonConvert.DeserializeObject<ObservableCollection<TermDeposit>>(content);
        }

        // Get request for stock history data webservice
        public static async Task FetchStockHistoryAsync(string symbol)
        {
            var res = await Client.GetAsync(BaseUrl + "getstockhistory" + "?stock=" + symbol);
            var content = await res.Content.ReadAsStringAsync();
            StockHistory = JsonConvert.DeserializeObject<ObservableCollection<StockHistory>>(content);
        }
    }
}
