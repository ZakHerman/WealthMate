using System;

namespace WealthMate.Models
{
    class OwnedStock : OwnedAsset
    {
        private float PurchasedPrice { get; set; }
        private float SharesPurchased { get; set; }
        private float CurrentPrice { get; }
        private float PriceClose { get; }
        //Elli: can we rename the underscore values, this is confusing!
        private float _dayReturn;
        //Elli: Updated DayReturn to protected so I can access from portfolio
        public float DayReturn
        {
            get
            {
                _dayReturn = CurrentValue - (PriceClose * SharesPurchased);
                return _dayReturn;
            }
        }
        private float _dayReturnRate;
        private float DayReturnRate
        {
            get
            {
                _dayReturnRate = (DayReturn / PrincipalValue) * 100;
                return _dayReturnRate;
            }
        }
        protected override float PrincipalValue
        {
            get
            {
                return PurchasedPrice * SharesPurchased;
            }
        }
        public override float CurrentValue
        {
            get
            {
                return CurrentPrice * SharesPurchased;
            }
        }

        private OwnedStock(ref string name, string purchaseDate, float purchasedPrice, float sharesPurchased, ref float currentPrice, ref float priceClose) : base(name, purchaseDate, "stock", 0, 0, 0, 0, 0)
        {
            PurchasedPrice = purchasedPrice;
            SharesPurchased = sharesPurchased;
            CurrentPrice = currentPrice;
            PriceClose = priceClose;
            _dayReturn = DayReturn;
            _dayReturnRate = DayReturnRate;
        }
    }
}