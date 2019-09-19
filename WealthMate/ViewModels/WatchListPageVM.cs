using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WealthMate.Models;
using Xamarin.Forms;

namespace WealthMate.ViewModels
{
    public class WatchListPageVM
    {
        public ObservableCollection<Stock> WatchListStocks { get; set; }
        public WatchListPageVM()
        {
            WatchListStocks = ((App)Application.Current).User.WatchListStocks;

            foreach (var stock in WatchListStocks)
                stock.UpdateStock();
        }
    }
}
