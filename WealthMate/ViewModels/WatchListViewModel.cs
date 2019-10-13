using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WealthMate.Models;
using Xamarin.Forms;

namespace WealthMate.ViewModels
{
    public class WatchListViewModel
    {
        public ObservableCollection<Stock> WatchListStocks { get; set; }
        public List<string> criteria { get; set; }

        public WatchListViewModel()
        {
            WatchListStocks = new ObservableCollection<Stock>();
            setList("Company Name");
            WatchListStocks = App.WatchList;

            foreach (var stock in WatchListStocks)
                stock.UpdateStock();
        }

        public List<string> getSortCriteria()
        {
            var criteria = new List<string>()
            {
                "Company Name", "Current Price", "Day Return Rate"
            };

            return criteria;
        }

        private string _selectedCriteria;
        public string SelectedCriteria
        {
            get { return _selectedCriteria; }
            set
            {
                if (_selectedCriteria != value)
                {
                    _selectedCriteria = value;

                    setList(_selectedCriteria);
                }
            }
        }

        private void setList(string picker)
        {
            //can clear and make new list if need be
             switch (picker)
            {
                case "Company Name":
                    sortList(WatchListStocks.OrderBy(stock => stock.CompanyName));           
                    break;
                case "Current Price":
                    sortList(WatchListStocks.OrderByDescending(stock => stock.CurrentPrice));
                    break;
                case "Day Return Rate":
                    sortList(WatchListStocks.OrderByDescending(t => t.DayReturnRate));
                    break;
            }
        }

        private void sortList(IOrderedEnumerable<Stock> linqResults)
        {
            var observableC = new ObservableCollection<Stock>(linqResults);
            WatchListStocks.Clear();
            foreach (Stock stock in observableC)
            {
                WatchListStocks.Add(stock);
            }
        }
    }
}
