using System;
using SQLite;

namespace WealthMate.Models
{
    public class OwnedStock : OwnedAsset
    {
        private float _purchasedPrice;
        private float _sharesPurchased;
        private float _currentPrice;
        private float _dayReturn;
        private float _dayReturnRate;

        [Ignore]
        public Stock Stock { get; set; } // Takes public stock into constructor to access it's updating variables (i.e. current price of stock).
        public string Symbol { get; set; }
        public float PurchasedPrice
        {
            get => _purchasedPrice;
            set
            {
                _purchasedPrice = value;
                OnPropertyChanged();
            }
        }
        public float SharesPurchased
        {
            get => _sharesPurchased;
            set
            {
                _sharesPurchased = value;
                OnPropertyChanged();
            }
        }
        public float CurrentPrice
        {
            get => _currentPrice;
            set
            {
                _currentPrice = value;
                OnPropertyChanged();
            }
        }

        // Returns made in a day
        public float DayReturn
        {
            get => _dayReturn;
            set
            {
                _dayReturn = value;
                OnPropertyChanged();
            }
        }

        // Percentage returns made in a day
        public float DayReturnRate
        {
            get => _dayReturnRate;
            set
            {
                _dayReturnRate = value;
                OnPropertyChanged();
            }
        }
        public bool PositiveDayReturns { get; set; }

        // Xamarin details page header displayed
        public string AssetNameTypePurchasedPrice => base.AssetNameType + " ($" + PurchasedPrice + ")";

        // OwnedStock Constructor
        public OwnedStock(Stock stock, DateTime purchaseDate, float purchasedPrice, float sharesPurchased, float returnGoal)
        {
            PurchasedPrice = purchasedPrice;
            SharesPurchased = sharesPurchased;
            Stock = stock;
            CurrentPrice = Stock.CurrentPrice; // Takes current price of the stock that is purchased
            PrincipalValue = PurchasedPrice * SharesPurchased; //How much the shares are initially worth
            AssetName = stock.CompanyName; // Transfers base class values
            PurchaseDate = purchaseDate;
            Type = "Stock";
            ReturnGoal = returnGoal;
            Symbol = stock.Symbol;
            UpdateOwnedAsset(); // Calculates all required values when constructed
        }

        public OwnedStock()
        {

        }

        // Creates up to date values for the Owned Asset
        public override void UpdateOwnedAsset()
        {
            UpdateCurrentDetails();
            UpdateDayReturnDetails();
            UpdateTotalReturnDetails();

            if (ReturnGoal == 0.0f)
                ReturnGoalProgress = 0.0f;
            else
                ReturnGoalProgress = (TotalReturn / ReturnGoal) * 100; // Updates how close the return value is to reaching its return goal

            if (ReturnGoalProgress <= 0)
                ReturnGoalProgress = 0.0f;
            else if (ReturnGoalProgress >= 100)
                ReturnGoalProgress = 100.0f;
        }

        private void UpdateTotalReturnDetails()
        {
            TotalReturn = CurrentValue - PrincipalValue;
            TotalReturnRate = (TotalReturn / PrincipalValue) * 100; // Converts total returns to percentage
            PositiveTotal = TotalReturn > 0;
        }

        private async void UpdateDayReturnDetails()
        {
            if (Stock == null)
                Stock = await App.Database.GetStockAsync(Symbol);

            // Current value less how much stock was worth at the start of the day
            DayReturn = CurrentValue - (Stock.PriceClose * SharesPurchased);
            // Converts day return into a percentage
            DayReturnRate = (DayReturn / PrincipalValue) * 100;
            PositiveDayReturns = DayReturn > 0;
        }

        // Updates price of stock before calculated how much it is currently worth
        private async void UpdateCurrentDetails()
        {
            if (Stock == null)
                Stock = await App.Database.GetStockAsync(Symbol);

            CurrentPrice = Stock.CurrentPrice;                                      
            CurrentValue = CurrentPrice * SharesPurchased;
        }

        // Alters the asset the owned stock the user is editing
        public void EditStock(float shares, float price, float returnGoal)
        {
            if ((SharesPurchased != shares) && (shares != 0))
                SharesPurchased = shares;

            if ((PurchasedPrice != price) && (price != 0))
                PurchasedPrice = price;

            if ((ReturnGoal != returnGoal) && (returnGoal != 0))
                ReturnGoal = returnGoal;

            PrincipalValue = SharesPurchased * PurchasedPrice;
            UpdateOwnedAsset();
        }
    }
}