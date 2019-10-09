using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WealthMate.Models;

namespace WealthMate.ViewModels
{
    public class WatchListPageVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Stock> _watchListStocks;

        public ObservableCollection<Stock> WatchListStocks {
            get => _watchListStocks;
            set
            {
                _watchListStocks = value;
                OnPropertyChanged();
            }

        }

        public WatchListPageVM()
        {
            WatchListStocks = App.WatchList;

            foreach (var stock in WatchListStocks)
                stock.UpdateStock();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
