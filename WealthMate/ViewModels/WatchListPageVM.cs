using System.Collections.ObjectModel;
using WealthMate.Models;
using Xamarin.Forms;

namespace WealthMate.ViewModels
{
    public class WatchListPageVM
    {
        public ObservableCollection<Stock> WatchListStocks { get; set; }
        public WatchListPageVM()
        {
            WatchListStocks = ((App)Application.Current).User.WatchListStocks;      //Captures watchlist stocks of user

            foreach (var stock in WatchListStocks)                                      //Makes sure all stocks are updated in watchlist
                stock.UpdateStock();
        }

        //Buttons still need to be implemented
    }
}
