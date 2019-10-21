using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        //attribute for observing the selected item in the picker
        //calls SetList() which sorts list according to picker value
        private string _selectedCriteria;
        public string SelectedCriteria
        {
            get => _selectedCriteria;
            set
            {
                if (_selectedCriteria != value)
                {
                    _selectedCriteria = value;

                    setList(_selectedCriteria);
                }
            }
        }

        // sorts WatchListStocks list according to picker value
        private void setList(string picker)
        {
            switch (picker)
            {
                case "Company Name":
                    sortList(WatchListStocks.OrderBy(stock => stock.CompanyName));           
                    break;
                case "Current Price (high-low)":
                    sortList(WatchListStocks.OrderByDescending(stock => stock.CurrentPrice));
                    break;
                case "Current Price (low-high)":
                    sortList(WatchListStocks.OrderBy(stock => stock.CurrentPrice));
                    break;
                case "Day Return Rate":
                    sortList(WatchListStocks.OrderByDescending(stock => stock.DayReturnRate));
                    break;
            }
        }


        //clears WatchListStocks list and re-adds assets to collection
        //this is required due to the return type of OrderBy and OrderByDescending methods
        private void sortList(IEnumerable<Stock> linqResults)
        {
            var observableC = new ObservableCollection<Stock>(linqResults);
            WatchListStocks.Clear();
            foreach (var stock in observableC)
            {
                WatchListStocks.Add(stock);
            }
        }
    }
}
