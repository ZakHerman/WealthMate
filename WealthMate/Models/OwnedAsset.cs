using System;

namespace WealthMate.Models
{
    public class OwnedAsset
    {
        public string AssetName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Type { get; set; }
        public int Length { get; set; }                             //In number of years
        public float InterestRate { get; set; }
        public int CompoundRate { get; set; }
        public float RegularPayment { get; set; }
        public virtual float PrincipalValue { get; set; }

        private float _currentValue;
        public virtual float CurrentValue
        {
            get
            {
                return _currentValue;
            }
            set
            {
                var DaysBetween = (DateTime.Today - PurchaseDate).TotalDays;

                var NonPaymentValue = PrincipalValue * (float)Math.Pow((1 + (InterestRate / CompoundRate)), (DaysBetween / 365) * CompoundRate);

                if (RegularPayment > 0)
                    _currentValue= (RegularPayment * (((float)Math.Pow((1 + (InterestRate / CompoundRate)), (DaysBetween / 365) * CompoundRate) - 1) / (InterestRate / CompoundRate))) + NonPaymentValue;
                else
                    _currentValue = NonPaymentValue;
            }
        }
        public float TotalReturn
        {
            get
            {
                return TotalReturn;
            }
            set
            {
                TotalReturn = CurrentValue - PrincipalValue;
            }
        }
        public float TotalReturnRate
        {
            get
            {
                return TotalReturnRate;
            }
            set
            {
                TotalReturnRate = (TotalReturn / PrincipalValue) * 100;
            }
        }

        public OwnedAsset(string assetName, DateTime purchaseDate, string type, float principalValue, float interestRate, int length, int compoundRate, float regularPayment)
        {
            AssetName = assetName;
            PurchaseDate = purchaseDate;
            Type = type;
            RegularPayment = regularPayment;
            PrincipalValue = principalValue;
            InterestRate = interestRate;
            Length = length;
            CompoundRate = compoundRate;
            CurrentValue = CurrentValue;
            TotalReturn = TotalReturn;
            TotalReturnRate = TotalReturnRate;
        }


    }
}