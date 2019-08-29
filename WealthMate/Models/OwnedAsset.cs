using System;

public class OwnedAsset
{
    private string AssetName { get; set; }
    private string PurchaseDate { get; set; }
    private string Type { get; set; }
    private int Length { get; set; }
    private float InterestRate { get; set; }
    private int CompoundRate { get; set; }
    private float RegularPayment { get; set; }
    protected virtual float PrincipalValue { get; set; }
    private float _currentValue;
    public virtual float CurrentValue
    {
        get
        {
            var NonPaymentValue = PrincipalValue * (float)Math.Pow((1 + (InterestRate / CompoundRate)), Length * CompoundRate);

            if (RegularPayment > 0)
                return (RegularPayment * (((float)Math.Pow((1 + (InterestRate / CompoundRate)), Length * CompoundRate) - 1 ) / (InterestRate / CompoundRate))) + NonPaymentValue;
            else
                return NonPaymentValue;
        }
    }
    private float _totalReturn;
    public float TotalReturn
    {
        get
        {
            _totalReturn = CurrentValue - PrincipalValue;
            return _totalReturn;
        }
    }
    private float _totalReturnRate;
    public float TotalReturnRate
    {
        get
        {
            _totalReturnRate = (TotalReturn / PrincipalValue) * 100;
            return _totalReturnRate;
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
        _currentValue = CurrentValue;
        _totalReturn = TotalReturn;
        _totalReturnRate = TotalReturnRate;
    }
}