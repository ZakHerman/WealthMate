using System;
using System.Collections.ObjectModel;

namespace WealthMate.Models
{
    public class StockHistory
    {
        public ObservableCollection<Stock> History{ get; } = new ObservableCollection<Stock>();

        public StockHistory()
        {
            GenerateExample();
        }

        private void GenerateExample()
        {
            History.Add(new Stock {PriceDate = new DateTime(2019,07,22), CurrentPrice = 1.23f});
            History.Add(new Stock {PriceDate = new DateTime(2019,07,23), CurrentPrice = 2.2f});
            History.Add(new Stock {PriceDate = new DateTime(2019,07,24), CurrentPrice = 4f});
            History.Add(new Stock {PriceDate = new DateTime(2019,07,25), CurrentPrice = 12.2f});
            History.Add(new Stock {PriceDate = new DateTime(2019,07,26), CurrentPrice = 5f});
            History.Add(new Stock {PriceDate = new DateTime(2019,07,27), CurrentPrice = 15f});
            History.Add(new Stock {PriceDate = new DateTime(2019,07,28), CurrentPrice = 3.25f});
            History.Add(new Stock {PriceDate = new DateTime(2019,07,29), CurrentPrice = 1.23f});
            History.Add(new Stock {PriceDate = new DateTime(2019,07,30), CurrentPrice = 1.23f});
            History.Add(new Stock {PriceDate = new DateTime(2019,07,31), CurrentPrice = 2.52f});
        }
    }
}
