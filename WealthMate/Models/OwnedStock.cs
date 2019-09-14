using System;

namespace WealthMate.Models
{
    public class OwnedStock : OwnedAsset
    {
        public Stock Stock { get; set; }
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
                DayReturn = CurrentValue - (Stock.PriceClose * SharesPurchased);
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
        public override float PrincipalValue //total price paid
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
                CurrentValue = Stock.CurrentPrice * SharesPurchased;
            }
        }

        public OwnedStock(Stock stock, DateTime purchaseDate, float purchasedPrice, float sharesPurchased) : base(stock.CompanyName, purchaseDate, "stock", 0, 0, 0, 0, 0)
        {
            PurchasedPrice = purchasedPrice;
            SharesPurchased = sharesPurchased;
            Stock = stock;
            DayReturn = DayReturn;
            DayReturnRate = DayReturnRate;
        }

        public OwnedStock()
        {

        }
    }
}