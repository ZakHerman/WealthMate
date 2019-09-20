using System;
using System.ComponentModel;

namespace WealthMate.Models
{
    public class OwnedAsset : INotifyPropertyChanged
    {
        public string AssetName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Type { get; set; }
        public int Length { get; set; }                             //In number of years
        public float InterestRate { get; set; }
        public int CompoundRate { get; set; }
        public float RegularPayment { get; set; }
        public float PrincipalValue { get; set; }
        public float CurrentValue { get; set;  }
        public float TotalReturn { get; set; }
        public float TotalReturnRate { get; set; }
        public bool PositiveTotal { get; set; }
        public string AssetNameType { get { return AssetName + " " + Type; } }
        public string CompoundRateToString { get; set; }
        public string InterestRateToString { get; set; }

        // Virtual member in constructor call
        // https://stackoverflow.com/questions/119506/virtual-member-call-in-a-constructor
        public OwnedAsset(string assetName, DateTime purchaseDate, string type, float principalValue, float interestRate, int length, int compoundRate, float regularPayment)
        {
            AssetName = assetName;
            PurchaseDate = purchaseDate;
            Type = type;
            RegularPayment = regularPayment;
            PrincipalValue = principalValue;
            InterestRate = interestRate;
            Length = length;
            CompoundRate = compoundRate; //how often per year that interest is calculated/added
            UpdateOwnedAsset();
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

        private void CalculateReturn()
        {
            TotalReturn = CurrentValue - PrincipalValue;
            TotalReturnRate = (TotalReturn / PrincipalValue) * 100;

            if (TotalReturn > 0f)                               //Flag for XAML code (green or red colours)
                PositiveTotal = true;
            else
                PositiveTotal = false;
        }

        private void CalculateCurrentValue()
        {
            TimeSpan daysBetween = DateTime.Today - PurchaseDate;

            var nonPaymentValue = PrincipalValue * (float)Math.Pow((1 + (InterestRate / CompoundRate)), (daysBetween.TotalDays / 365.25) * CompoundRate);

            if (RegularPayment > 0)
                CurrentValue = (RegularPayment * (((float)Math.Pow((1 + (InterestRate / CompoundRate)), 
                    (daysBetween.TotalDays / 365.25) * CompoundRate) - 1) / (InterestRate / CompoundRate))) + nonPaymentValue;
            else
                CurrentValue = nonPaymentValue;
        }

        // Alters the asset the user is editing
        public void EditAsset(float interestRate, int length, float regularPayment, OwnedAsset ownedAsset)
        {
            if ((ownedAsset.InterestRate != interestRate) && (interestRate != 0))
            {
                ownedAsset.InterestRate = interestRate;
            }

            if ((ownedAsset.Length != length) && (length != 0))
            {
                ownedAsset.Length = length;
            }

            if ((ownedAsset.RegularPayment != regularPayment) && (regularPayment != 0))
            {
                ownedAsset.RegularPayment = regularPayment;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));

        }

    }
}