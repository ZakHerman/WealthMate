using System;

namespace WealthMate.Models
{
    public class OwnedAsset
    {
        private float _currentValue;
        private float _totalReturn;
        private float _totalReturnRate;
        private bool _positiveTotal;


        public string AssetName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Type { get; set; }
        public string AssetNameType { get { return AssetName + " " + Type; } }
        public int Length { get; set; }                             //In number of years
        public float InterestRate { get; set; }
        public int CompoundRate { get; set; }
        public float RegularPayment { get; set; }
        public virtual float PrincipalValue { get; set; }
        
        // Setter should be using value keyword
        // Replace set code with a method in compound interest method (possibly in a separate static class)
        // Interest calculation will be incorrect when daysBetween includes leap years
        public virtual float CurrentValue
        {
            get => _currentValue;
            set
            {
                TimeSpan daysBetween = DateTime.Today - PurchaseDate;

                var nonPaymentValue = PrincipalValue * (float)Math.Pow((1 + (InterestRate / CompoundRate)), (daysBetween.TotalDays / 365.25) * CompoundRate);

                if (RegularPayment > 0)
                    _currentValue= (RegularPayment * (((float)Math.Pow((1 + (InterestRate / CompoundRate)), (daysBetween.TotalDays / 365.25) * CompoundRate) - 1) / (InterestRate / CompoundRate))) + nonPaymentValue;
                else
                    _currentValue = nonPaymentValue;
            }
        }

        // Setter should be using value keyword
        // Currently can not set these properties
        public float TotalReturn
        {
            get => _totalReturn;
            set => _totalReturn = CurrentValue - PrincipalValue;
        }

        // Setter should be using value keyword
        public float TotalReturnRate
        {
            get => _totalReturnRate;
            set => _totalReturnRate = (TotalReturn / PrincipalValue) * 100;
        }

        public bool PositiveTotal
        {
            get => _positiveTotal;
            set
            {
                if (_totalReturn > 0f)
                    _positiveTotal = true;
                else
                    _positiveTotal = false;
            }
        }

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
            CurrentValue = _currentValue;
            TotalReturn = _totalReturn;
            TotalReturnRate = _totalReturnRate;
            PositiveTotal = _positiveTotal;
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
            TotalReturn = TotalReturn;
            TotalReturnRate = TotalReturnRate;
        }
    }
}