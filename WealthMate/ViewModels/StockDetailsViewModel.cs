using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Plugin.Toast;
using WealthMate.Models;
using Xamarin.Forms;

namespace WealthMate.ViewModels
{
    public class StockDetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _watchListImage;

        public Stock Stock { get; }
        public ObservableCollection<StockHistory> StockHistory { get; set; }
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

        private async void LoadStockHistory()
        {
            StockHistory = new ObservableCollection<StockHistory>(await App.Database.GetStockHistoryAsync(Stock.Symbol));
            OnPropertyChanged(nameof(StockHistory));
        }

        public async void AddToWatchList()
        {
            Watched = !Watched;

            if (Watched)
            {
                WatchListImage = "starfilled.png";
                WatchListStocks.Add(Stock);
                await App.Database.SaveWatchListAsync(new WatchedStock{Symbol = Stock.Symbol});

                Application.Current.Resources.TryGetValue("ToastNotificationBackgroundColor", out var bgColor);
                Application.Current.Resources.TryGetValue("PrimaryTextColor", out var textColor);

                if (bgColor != null && textColor != null)
                    CrossToastPopUp.Current.ShowCustomToast("Added to watchlist", ((Color)bgColor).ToHex(), ((Color)textColor).ToHex());
                else
                    CrossToastPopUp.Current.ShowCustomToast("Added to watchlist", "#CC212121", "#FFFFFF");
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
