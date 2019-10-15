using System;
using System.Collections.Generic;
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
        private static readonly HttpClient Client = new HttpClient();
        private const string BaseUrl = "https://wealthmate.azurewebsites.net/api/";

        public static ObservableCollection<Stock> Stocks { get; private set; }
        public static ObservableCollection<TermDeposit> TermDeposits { get; private set; }

        // Get request to stocks data webservice
        public static async Task FetchStocksAsync()
        {
            var res = await Client.GetAsync(BaseUrl + "getstocksnzx");
            var content = await res.Content.ReadAsStringAsync();
            Stocks = JsonConvert.DeserializeObject<ObservableCollection<Stock>>(content);

            foreach (var stock in Stocks)
            {
                stock.UpdateStock();
                await App.Database.SaveStockAsync(stock);
            }
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
        public static async Task<List<StockHistory>> FetchStockHistoryAsync(string symbol, DateTime dateFrom = new DateTime())
        {
            var res = await Client.GetAsync($"{BaseUrl}getstockhistory?stock={symbol}&date_from={dateFrom:yyyy-MM-dd}");
            var content = await res.Content.ReadAsStringAsync();

            var stockHistory = JsonConvert.DeserializeObject<List<StockHistory>>(content);
            await App.Database.SaveStockHistoryAsync(stockHistory);

            Debug.WriteLine($"Remote database ({symbol}). Results: {stockHistory.Count}, date to check: {App.Database.PreviousTradingDate:yyyy-MM-dd}");

            return stockHistory;
        }
    }
}
