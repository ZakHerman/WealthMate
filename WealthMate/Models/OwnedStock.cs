using System;

namespace WealthMate.Models
{
    class OwnedStock : OwnedAsset
    {
        public float PurchasedPrice { get; set; }
        public float SharesPurchased { get; set; }
        public float CurrentPrice { get; }
        public float PriceClose { get; }
        public float DayReturn
        {
            get
            {
                return DayReturn;
            }
            set
            {
                DayReturn = CurrentValue - (PriceClose * SharesPurchased);
            }
        }
        public float DayReturnRate
        {
            get
            {
                return DayReturnRate;
            }
            set
            {
                DayReturnRate = (DayReturn / PrincipalValue) * 100;
            }
        }
        public override float PrincipalValue
        {
            get
            {
                return PrincipalValue;
            }
            set
            {
                PrincipalValue = PurchasedPrice * SharesPurchased;
            }
        }
        public override float CurrentValue
        {
            get
            {
                return CurrentValue;
            }
            set
            {
                CurrentValue = CurrentPrice * SharesPurchased;
            }
        }

        public OwnedStock(ref string name, string purchaseDate, float purchasedPrice, float sharesPurchased, ref float currentPrice, ref float priceClose) : base(name, purchaseDate, "stock", 0, 0, 0, 0, 0)
        {
            PurchasedPrice = purchasedPrice;
            SharesPurchased = sharesPurchased;
            CurrentPrice = currentPrice;
            PriceClose = priceClose;
            DayReturn = DayReturn;
            DayReturnRate = DayReturnRate;
        }
    }
}