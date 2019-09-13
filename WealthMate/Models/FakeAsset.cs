using System;
using System.Collections.Generic;
using System.Text;

namespace WealthMate.Models
{
    public class FakeAsset
    {
        public string StockName { get; set; }
        public float Value { get; set; }
        public string AssetType { get; set; }
        public float PercentageChange { get; set; }
    }
}
