using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace WealthMate.Models
{
    public class StockHistory
    {
        public ObservableCollection<Stock> History{ get; } = new ObservableCollection<Stock>();
        readonly Random _rng = new Random();

        public StockHistory()
        {
            GenerateExample();
        }

        private void GenerateExample()
        {
            History.Add(new Stock {PriceDate = new DateTime(2019,07,22), CurrentPrice = _rng.Next(1, 15)});
            History.Add(new Stock {PriceDate = new DateTime(2019,07,23), CurrentPrice = _rng.Next(1, 15)});
            History.Add(new Stock {PriceDate = new DateTime(2019,07,24), CurrentPrice = _rng.Next(1, 15)});
            History.Add(new Stock {PriceDate = new DateTime(2019,07,25), CurrentPrice = _rng.Next(1, 15)});
            History.Add(new Stock {PriceDate = new DateTime(2019,07,26), CurrentPrice = _rng.Next(1, 15)});
            History.Add(new Stock {PriceDate = new DateTime(2019,07,27), CurrentPrice = _rng.Next(1, 15)});
            History.Add(new Stock {PriceDate = new DateTime(2019,07,28), CurrentPrice = _rng.Next(1, 15)});
            History.Add(new Stock {PriceDate = new DateTime(2019,07,29), CurrentPrice = _rng.Next(1, 15)});
            History.Add(new Stock {PriceDate = new DateTime(2019,07,30), CurrentPrice = _rng.Next(1, 15)});
            History.Add(new Stock {PriceDate = new DateTime(2019,07,31), CurrentPrice = _rng.Next(1, 15)});
        }
    }
}
