using System;

namespace WealthMate.Models
{
    public class OwnedStock : OwnedAsset
    {
        private float _dayReturn;
        private float _dayReturnRate;
        private float _principalValue;
        private float _currentValue;

        public Stock Stock { get; set; }
        public float PurchasedPrice { get; set; }
        public float SharesPurchased { get; set; }

        // Get only properties are never assigned
        public float CurrentPrice { get; }
        public float PriceClose { get; }

        // Setter should be using value keyword
        // Currently can not set these properties
        public float DayReturn
        {
            get => _dayReturn;
            set => _dayReturn = CurrentValue - (Stock.PriceClose * SharesPurchased);
        }

        // Setter should be using value keyword
        // Redundant parentheses
        public float DayReturnRate
        {
            get => _dayReturnRate;
            set => _dayReturnRate = (DayReturn / PrincipalValue) * 100;
        }

        // Setter should be using value keyword
        public override float PrincipalValue //total price paid
        {
            get => _principalValue;
            set => _principalValue = PurchasedPrice * SharesPurchased;
        }

        // Setter should be using value keyword
        public override float CurrentValue
        {
            get => _currentValue;
            set => _currentValue = Stock.CurrentPrice * SharesPurchased;
        }

        // Unnecessary use of base class as nothing is being used from it and first parameter is passing companyname for AssetName
        public OwnedStock(Stock stock, DateTime purchaseDate, float purchasedPrice, float sharesPurchased) : base(stock.CompanyName, purchaseDate, "stock", 0, 0, 0, 0, 0)
        {
            PurchasedPrice = purchasedPrice;
            SharesPurchased = sharesPurchased;
            Stock = stock;
            DayReturn = DayReturn;
            DayReturnRate = DayReturnRate;
        }
    }
}