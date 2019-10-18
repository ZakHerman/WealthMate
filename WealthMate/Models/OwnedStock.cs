using System;

namespace WealthMate.Models
{
    public class OwnedStock : OwnedAsset
    {
        public Stock Stock { get; set; }                        //Takes public stock into constructor to access it's updating variables (i.e. current price of stock).
        public float PurchasedPrice { get; set; }               //Price ownedstock was purchased at
        public int SharesPurchased { get; set; }                //Amount of shares the ownedstock owns
        public float CurrentPrice { get; set; }                 //Current price of OwnedStcok
        public float DayReturn { get; set; }                    //Returns made in a day
        public float DayReturnRate { get; set; }                //Percentage returns made in a day
        public bool PositiveDayReturns { get; set; }            //Flag for xamarin view page that determines the text colour (green/red) to display
        public string AssetNameTypePurchasedPrice { get { return base.AssetNameType + " ($" + PurchasedPrice + ")"; } }     //Xamarin details page header displayed

        //OwnedStock Constructor
        public OwnedStock(Stock stock, DateTime purchaseDate, float purchasedPrice, int sharesPurchased, float returnGoal)
        {
            PurchasedPrice = purchasedPrice;
            SharesPurchased = sharesPurchased;
            Stock = stock;
            CurrentPrice = Stock.CurrentPrice;                                  //Takes current price of the stock that is purchased
            PrincipalValue = PurchasedPrice * SharesPurchased;                  //How much the shares are initially worth
            AssetName = stock.CompanyName;                                      //Transfers base class values
            PurchaseDate = purchaseDate;
            Type = "Stock";
            ReturnGoal = returnGoal;
            UpdateOwnedAsset();                                                 //Calculates all required values when constructed
        }

        //Overloading constructor for testing purposes --> planning to remove
        public OwnedStock()
        {
            Type = "Stock";
        }

        // Creates up to date values for the Owned Asset
        public override void UpdateOwnedAsset()
        {
            UpdateCurrentDetails();                                                     //Updates current details of owned asset
            UpdateDayReturnDetails();                                                   //Updates the returns made during the day
            UpdateTotalReturnDetails();                                                 //Updates the total returns made on the owned stock
            ReturnGoalProgress = (TotalReturn / ReturnGoal) * 100;                      //Updates how close the return value is to reaching its return goal
        }

        private void UpdateTotalReturnDetails()
        {
            TotalReturn = CurrentValue - PrincipalValue;
            TotalReturnRate = (TotalReturn / PrincipalValue) * 100;                     //Converts total returns to percentage
            PositiveTotal = TotalReturn > 0;                                            //Updates xamarin view flag
        }

        private void UpdateDayReturnDetails()
        {
            DayReturn = CurrentValue - (Stock.PriceClose * SharesPurchased);            //Current value less how much stock was worth at the start of the day
            DayReturnRate = (DayReturn / PrincipalValue) * 100;                         //Converts day return into a percentage
            PositiveDayReturns = DayReturn > 0;                                         //Updates xamarin view flag
        }

        private void UpdateCurrentDetails()                                             //Updates price of stock before calculated how much it is currently worth
        {
            CurrentPrice = Stock.CurrentPrice;                                      
            CurrentValue = CurrentPrice * SharesPurchased;
        }

        // Alters the asset the owned stock the user is editing
        public void EditStock(int shares, float price, OwnedStock ownedStock)
        {
            if (ownedStock.SharesPurchased != shares && shares != 0)
            {
                ownedStock.SharesPurchased = shares;
            }

            if (ownedStock.PurchasedPrice != price && price != 0)
            {
                ownedStock.PurchasedPrice = price;
            }

            ownedStock.UpdateOwnedAsset();

            //this.UpdateOwnedAsset();
        }

    }
}