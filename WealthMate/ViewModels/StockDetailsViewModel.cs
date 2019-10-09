using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WealthMate.Models;
using WealthMate.Services;
using Xamarin.Forms;

namespace WealthMate.ViewModels
{
    public class StockDetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _watchListImage;

        public Stock Stock { get; }
        public ObservableCollection<StockHistory> StockHistory { get; set; }
        public ObservableCollection<Stock> WatchListStocks { get; set; } = ((App)Application.Current).User.WatchListStocks;
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

            Watched = WatchListStocks.Contains(stock);
            WatchListCommand = new Command(AddToWatchList);
            WatchListImage = Watched ? "starfilled.png" : "starunfilled.png";
        }

        private async void LoadStockHistory()
        {
            await DataService.FetchStockHistoryAsync(Stock.Symbol);
            StockHistory = DataService.StockHistory;
            OnPropertyChanged(nameof(StockHistory));
        }

        public void AddToWatchList()
        {
            Watched = !Watched;

            if (Watched)
            {
                WatchListImage = "starfilled.png";
                WatchListStocks.Add(Stock);
            }
            else
            {
                WatchListImage = "starunfilled.png";
                WatchListStocks.Remove(Stock);
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
