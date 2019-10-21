using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WealthMate.Helpers;
using WealthMate.Models;
using Xamarin.Forms;

namespace WealthMate.ViewModels
{
    public class StockDetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _watchListImage;
        private static readonly Dictionary<string, List<StockHistory>> StockHistoryDictionary = new Dictionary<string, List<StockHistory>>();

        public int Minimum { get; set; }
        public Stock Stock { get; }
        public List<StockHistory> StockHistory { get; set; }
        public ObservableCollection<Stock> WatchListStocks { get; set; } = App.WatchList;
        public bool Watched { get; set; }
        public ICommand WatchListCommand { get; }
        public string WatchListImage
        {
            get => _watchListImage;
            set
            {
                _watchListImage = value;
                OnPropertyChanged();
            }
        }

        public StockDetailsViewModel(Stock stock)
        {
            Stock = stock;
            stock.UpdateStock();
            LoadStockHistory();

            Watched = WatchListStocks.Any(s => s.Symbol == Stock.Symbol);
            WatchListCommand = new Command(AddToWatchList);
            WatchListImage = Watched ? "starfilled.png" : "starunfilled.png";
        }

        // Check if stock history is stored in memory first otherwise check database
        private async void LoadStockHistory()
        {
            if (StockHistoryDictionary.TryGetValue(Stock.Symbol, out var history))
            {
                StockHistory = history;
            }
            else
            {
                history = await App.Database.GetStockHistoryAsync(Stock.Symbol);
                StockHistoryDictionary.Add(Stock.Symbol, history);
                StockHistory = history;
            }

            // Used to override area chart minimum Y-axis value
            Minimum = history.Any() ? (int)history.Min(s => s.PriceClose) : 0;
 
            OnPropertyChanged(nameof(StockHistory));
            OnPropertyChanged(nameof(Minimum));
        }

        public async void AddToWatchList()
        {
            Watched = !Watched;

            if (Watched)
            {
                WatchListImage = "starfilled.png";
                WatchListStocks.Add(Stock);
                await App.Database.SaveWatchListAsync(new WatchedStock{Symbol = Stock.Symbol});

                // Display toast notification when stock added to watchlist
                Helper.DisplayToastNotification("Added to watchlist");
            }
            else
            {
                WatchListImage = "starunfilled.png";
                var temp = WatchListStocks.Where(s => s.Symbol == Stock.Symbol).ToList();

                foreach (var remove in temp)
                {
                    WatchListStocks.Remove(remove);
                }

                await App.Database.DeleteWatchListAsync(new WatchedStock{Symbol = Stock.Symbol});
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
