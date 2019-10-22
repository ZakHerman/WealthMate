using System.Collections.ObjectModel;

namespace WealthMate.Models
{
    public class User
    {
        public Portfolio Portfolio { get; set; } // Each user has their own customized portfolio
        public ObservableCollection<Stock> WatchListStocks { get; set; } // Each user has their own customized watchlist

        public User()
        {
            Portfolio = new Portfolio();
            WatchListStocks = new ObservableCollection<Stock>();
        }
    }
}
