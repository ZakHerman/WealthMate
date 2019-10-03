using System;

namespace WealthMate.Models
{
    public class OwnedStock : OwnedAsset
    {
        public Stock Stock { get; set; }                        //Takes public stock into constructor to access it's updating variables.
        public float PurchasedPrice { get; set; }               //Price ownedstock was purchased at
        public int SharesPurchased { get; set; }              //Amount of shares the ownedstock owns
        public float CurrentPrice { get; set; }         
        public float DayReturn { get; set; }                    //Returns made in a day
        public float DayReturnRate { get; set; }
        public bool PositiveDayReturns { get; set; }            //Flag for view page trigger to determine colour and image to display
        public string AssetNameTypePurchasedPrice { get { return base.AssetNameType + " ($" + PurchasedPrice + ")"; } } //OwnedStock details page header display

        public OwnedStock(Stock stock, DateTime purchaseDate, float purchasedPrice, int sharesPurchased, float returnGoal)
        {
            PurchasedPrice = purchasedPrice;
            SharesPurchased = sharesPurchased;
            Stock = stock;
            CurrentPrice = Stock.CurrentPrice;
            PrincipalValue = PurchasedPrice * SharesPurchased;
            AssetName = stock.CompanyName;                     //Transfers base class values
            PurchaseDate = purchaseDate;
            Type = "Stock";
            ReturnGoal = returnGoal;
            UpdateOwnedAsset();
        }

        //Overloading constructor for testing purposes --> planning to remove
        public OwnedStock()
        {
            Type = "Stock";
        }

        // Creates up to date values for the Owned Asset
        public override void UpdateOwnedAsset()
        {
            UpdateCurrentDetails();
            UpdateDayReturnDetails();
            UpdateTotalReturnDetails();
            ReturnGoalProgress = (TotalReturn / ReturnGoal) * 100;
        }

        private void UpdateTotalReturnDetails()
        {
            TotalReturn = CurrentValue - PrincipalValue;
            TotalReturnRate = (TotalReturn / PrincipalValue) * 100;
            PositiveTotal = TotalReturn > 0;
        }

        private void UpdateDayReturnDetails()
        {
            DayReturn = CurrentValue - (Stock.PriceClose * SharesPurchased);
            DayReturnRate = (DayReturn / PrincipalValue) * 100;
            PositiveDayReturns = DayReturn > 0;
        }

        private void UpdateCurrentDetails()
        {
            CurrentPrice = Stock.CurrentPrice;
            CurrentValue = CurrentPrice * SharesPurchased;
        }

        // Alters the asset the owned stock the user is editing
        public void EditStock(int shares, float price, OwnedStock ownedStock)
        {
            if (ownedStock.SharesPurchased != shares && shares != 0)
                ownedStock.SharesPurchased = shares;

            if (ownedStock.PurchasedPrice != price && price != 0)
                ownedStock.PurchasedPrice = price;
        }
    }
}