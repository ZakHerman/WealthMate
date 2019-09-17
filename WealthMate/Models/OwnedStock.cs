using System;

namespace WealthMate.Models
{
    public class OwnedStock : OwnedAsset
    {
        public Stock Stock { get; set; }
        public float PurchasedPrice { get; set; }
        public float SharesPurchased { get; set; }
        public float CurrentPrice { get; set; }
        public float DayReturn { get; set; }
        public float DayReturnRate { get; set; }
        public string AssetNameTypePurchasedPrice { get { return base.AssetNameType + " ($" + PurchasedPrice + ")"; } }

        // Unnecessary use of base class as nothing is being used from it and first parameter is passing companyname for AssetName
        public OwnedStock(Stock stock, DateTime purchaseDate, float purchasedPrice, float sharesPurchased)
        {
            PurchasedPrice = purchasedPrice;
            SharesPurchased = sharesPurchased;
            Stock = stock;
            CurrentPrice = Stock.CurrentPrice;
            PrincipalValue = PurchasedPrice * SharesPurchased;
            base.AssetName = stock.CompanyName;
            base.PurchaseDate = purchaseDate;
            base.Type = "Stock";
            UpdateOwnedAsset();
        }

        public OwnedStock()
        {

        }

        public override void UpdateOwnedAsset()
        {
            CurrentPrice = Stock.CurrentPrice;
            CurrentValue = CurrentPrice * SharesPurchased;
            DayReturn = CurrentValue - (Stock.PriceClose * SharesPurchased);
            DayReturnRate = (DayReturn / PrincipalValue) * 100;
            TotalReturn = CurrentValue - PrincipalValue;
            TotalReturnRate = (TotalReturn / PrincipalValue) * 100;
             if (TotalReturn > 0f)
                PositiveTotal = true;
            else
                PositiveTotal = false;
        }
    }
}