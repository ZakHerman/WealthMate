using System;

namespace WealthMate.Models
{
    public class OwnedAsset
    {
        public string AssetName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Type { get; set; }                                    //The type of OwnedAsset
        public int Length { get; set; }                             //In number of years
        public float InterestRate { get; set; }
        public int CompoundRate { get; set; }                               //The amount of times interest is being compounded during a year
        public float RegularPayment { get; set; }
        public float PrincipalValue { get; set; }                               //The initial value (first value) of the owned asset
        public float CurrentValue { get; set;  }
        public float TotalReturn { get; set; }
        public float TotalReturnRate { get; set; }
        public float ReturnGoal { get; set; }
        public float ReturnGoalProgress { get; set; }
        public bool PositiveTotal { get; set; }                                     //Boolean for View page trigger.
        public string AssetNameType { get { return AssetName + " " + Type; } }  //To String for NavBar Title when selecting an OwnedAsset
        public string CompoundRateToString { get; set; }                        //Converts amount of time interest is being compounded into string
        public string InterestRateToString { get; set; }                        //Converts interest rate into readable string


        public OwnedAsset(string assetName, DateTime purchaseDate, string type, float principalValue, float interestRate, int length, int compoundRate, float regularPayment, float returnGoal)
        {
            AssetName = assetName;
            PurchaseDate = purchaseDate;
            Type = type;
            RegularPayment = regularPayment;
            PrincipalValue = principalValue;
            InterestRate = interestRate;
            Length = length;
            CompoundRate = compoundRate;
            ReturnGoal = returnGoal;
            UpdateOwnedAsset();                                     //Asset needs to be updated (return values) as soon as it is constructed.
        }

        // Virtual member in constructor call
        //Default constructor - temporary!
        public OwnedAsset()
        {
            AssetName = "Unknown";
            PurchaseDate = new DateTime(2019, 1, 1, 0, 0, 0);
            Type = "Unknown";
            RegularPayment = 0f;
            PrincipalValue = 0f;
            InterestRate = 0f;
            CompoundRate = 1; //how often per year that interest is calculated/added
            CurrentValue = 0f;
        }

        public virtual void UpdateOwnedAsset()
        {
            CalculateCurrentValue();
            CalculateReturn();
            CompoundRateConvert();
            ReturnGoalProgress = (TotalReturn / ReturnGoal)  * 100;       //Updates how close the value is to reaching its goal
            InterestRateToString = (InterestRate * 100).ToString();     //Changes float value (for calculation purposes) to readable percentage value
        }

        private void CompoundRateConvert()
        {
            switch (CompoundRate)                                   //Converts float value (for calculation purposes) into what it means.
            {
                case 1:
                    CompoundRateToString = "Annually";
                    break;
                case 2:
                    CompoundRateToString = "Semi-Annually";
                    break;
                case 4:
                    CompoundRateToString = "Quarterly";
                    break;
                case 12:
                    CompoundRateToString = "Monthly";
                    break;
                default:
                    CompoundRateToString = "Never";
                    break;
            }
        }

        private void CalculateReturn()                                  //Calculates the Total Return of the Owned Asset
        {
            TotalReturn = CurrentValue - PrincipalValue;
            TotalReturnRate = (TotalReturn / PrincipalValue) * 100;

            if (TotalReturn > 0f)                               //Flag for XAML code (green or red colours)
                PositiveTotal = true;
            else
                PositiveTotal = false;
        }

        private void CalculateCurrentValue()                    //Uses financial calculation to calculate today's value of the asset from when it was first acquired.
        {
            TimeSpan daysBetween = DateTime.Today - PurchaseDate;       //Calculates days between from when asset was purchased to today

            var nonPaymentValue = PrincipalValue * (float)Math.Pow((1 + (InterestRate / CompoundRate)), (daysBetween.TotalDays / 365.25) * CompoundRate);   //Does not take regular payments into account

            if (RegularPayment > 0)
                CurrentValue = (RegularPayment * (((float)Math.Pow((1 + (InterestRate / CompoundRate)), 
                    (daysBetween.TotalDays / 365.25) * CompoundRate) - 1) / (InterestRate / CompoundRate))) + nonPaymentValue;      //Takes regular payments into account
            else
                CurrentValue = nonPaymentValue;
        }

        // Alters the asset the user is editing
        public void EditAsset(float interestRate, int length, float regularPayment, OwnedAsset ownedAsset)
        {
            if ((ownedAsset.InterestRate != interestRate) && (interestRate != 0))
                ownedAsset.InterestRate = interestRate;

            if ((ownedAsset.Length != length) && (length != 0))
                ownedAsset.Length = length;

            if ((ownedAsset.RegularPayment != regularPayment) && (regularPayment != 0))
                ownedAsset.RegularPayment = regularPayment;
        }
    }
}