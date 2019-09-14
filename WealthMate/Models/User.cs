using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace WealthMate.Models
{
    public class User
    {
        public Portfolio Portfolio { get; set; }
        public ObservableCollection<Stock> WatchListStocks { get; set; }

        public User()
        {
            Portfolio = new Portfolio();
            WatchListStocks = new ObservableCollection<Stock>();
        }
    }
}
