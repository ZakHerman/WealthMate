using System.Collections.ObjectModel;

namespace WealthMate.Models
{
    public class User
    {
        // Each user has their own customized portfolio
        public Portfolio Portfolio { get; set; }
        public ObservableCollection<Stock> WatchListStocks { get; set; }

        public User()
        {
            Portfolio = new Portfolio();
            WatchListStocks = new ObservableCollection<Stock>();
        }
    }
}
