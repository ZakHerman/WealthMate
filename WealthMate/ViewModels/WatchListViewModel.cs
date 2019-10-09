using System.Collections.ObjectModel;
using WealthMate.Models;

namespace WealthMate.ViewModels
{
    public class WatchListViewModel
    {
        public ObservableCollection<Stock> WatchListStocks { get; set; }

        public WatchListViewModel()
        {
            WatchListStocks = App.WatchList;

            foreach (var stock in WatchListStocks)
                stock.UpdateStock();
        }
    }
}
