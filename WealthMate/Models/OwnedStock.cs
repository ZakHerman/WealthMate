﻿using System;

namespace WealthMate.Models
{
    public class OwnedStock : OwnedAsset
    {
        private float _purchasedPrice;
        private float _sharesPurchased;
        private float _currentPrice;
        private float _dayReturn;
        private float _dayReturnRate;
        public Stock Stock { get; set; }                        //Takes public stock into constructor to access it's updating variables (i.e. current price of stock).
        public float PurchasedPrice               //Price ownedstock was purchased at
        {
            get => _purchasedPrice;
            set
            {
                _purchasedPrice = value;
                OnPropertyChanged();
            }
        }
        public float SharesPurchased             //Amount of shares the ownedstock owns
        {
            get => _sharesPurchased;
            set
            {
                _sharesPurchased = value;
                OnPropertyChanged();
            }
        }
        public float CurrentPrice                 //Current price of OwnedStcok
        {
            get => _currentPrice;
            set
            {
                _currentPrice = value;
                OnPropertyChanged();
            }
        }
        public float DayReturn                  //Returns made in a day
        {
            get => _dayReturn;
            set
            {
                _dayReturn = value;
                OnPropertyChanged();
            }
        }
        public float DayReturnRate           //Percentage returns made in a day
        {
            get => _dayReturnRate;
            set
            {
                _dayReturnRate = value;
                OnPropertyChanged();
            }
        }
        public bool PositiveDayReturns { get; set; }            //Flag for xamarin view page that determines the text colour (green/red) to display
        public string AssetNameTypePurchasedPrice { get { return base.AssetNameType + " ($" + PurchasedPrice + ")"; } }     //Xamarin details page header displayed

        //OwnedStock Constructor
        public OwnedStock(Stock stock, DateTime purchaseDate, float purchasedPrice, float sharesPurchased, float returnGoal)
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

        public OwnedStock()
        {

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