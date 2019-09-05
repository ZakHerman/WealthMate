using System;

namespace WealthMate.Models
{
    public class OwnedAsset
    {
        public string AssetName { get; set; }
        public string PurchaseDate { get; set; }
        public string Type { get; set; }
        public int Length { get; set; }
        public float InterestRate { get; set; }
        public int CompoundRate { get; set; }
        public float RegularPayment { get; set; }
        public virtual float PrincipalValue { get; set; }
        public virtual float CurrentValue
        {
            get
            {
                return CurrentValue;
            }
            set
            {
                var NonPaymentValue = PrincipalValue * (float)Math.Pow((1 + (InterestRate / CompoundRate)), Length * CompoundRate);

                if (RegularPayment > 0)
                    CurrentValue = (RegularPayment * (((float)Math.Pow((1 + (InterestRate / CompoundRate)), Length * CompoundRate) - 1) / (InterestRate / CompoundRate))) + NonPaymentValue;
                else
                    CurrentValue = NonPaymentValue;
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

        public OwnedAsset(string assetName, string purchaseDate, string type, float principalValue, float interestRate, int length, int compoundRate, float regularPayment)
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