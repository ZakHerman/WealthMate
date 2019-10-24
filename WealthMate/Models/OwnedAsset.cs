using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLite;

namespace WealthMate.Models
{
    public class OwnedAsset : INotifyPropertyChanged
    {
        private float _returnGoal;                  
        private float _returnGoalProgress;
        private DateTime _purchaseDate;
        private float _length;
        private int _compoundRate;
        private float _regularPayment;
        private float _principalValue;
        private float _currentValue;
        private float _totalReturn;
        private float _totalReturnRate;
        private string _compoundRateToString;
        private string _interestRateToString;

        public event PropertyChangedEventHandler PropertyChanged;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string AssetName { get; set; }
        public float InterestRate { get; set; }
        public string Type { get; set; }
        public DateTime PurchaseDate
        {
            get => _purchaseDate;
            set
            {
                _purchaseDate = value;
                OnPropertyChanged();
            }
        }
        public float Length //In number of years
        {
            get => _length;
            set
            {
                _length = value;
                OnPropertyChanged();
            }
        }

        //The amount of times interest is being compounded during a year
        public int CompoundRate
        {
            get => _compoundRate;
            set
            {
                _compoundRate = value;
                OnPropertyChanged();
            }
        }

        //Regular payments that are put toward the owned asset.
        public float RegularPayment
        {
            get => _regularPayment;
            set
            {
                _regularPayment = value;
                OnPropertyChanged();
            }
        }

        //The initial value (first value) of the owned asset
        public float PrincipalValue
        {
            get => _principalValue;
            set
            {
                _principalValue = value;
                OnPropertyChanged();
            }
        }
        public float CurrentValue
        {
            get => _currentValue;
            set
            {
                _currentValue = value;
                OnPropertyChanged();
            }
        }
        public float TotalReturn
        {
            get => _totalReturn;
            set
            {
                _totalReturn = value;
                OnPropertyChanged();
            }
        }
        public float TotalReturnRate
        {
            get => _totalReturnRate;
            set
            {
                _totalReturnRate = value;
                OnPropertyChanged();
            }
        }

        //The amount of returns that the user wants to achieve with this asset
        public float ReturnGoal
        {
            get => _returnGoal;
            set
            {
                _returnGoal = value;
                OnPropertyChanged();
            }
        }

        //The percentage amount it is to its desired goal
        public float ReturnGoalProgress
        {
            get => _returnGoalProgress;
            set
            {
                _returnGoalProgress = value;
                OnPropertyChanged();
            }
        }
        public bool PositiveTotal { get; set; } //Boolean for View page trigger.
        public string AssetNameType => AssetName + " " + Type; //To String for NavBar Title when selecting an OwnedAsset

        //Converts amount of time interest is being compounded into string
        public string CompoundRateToString
        {
            get => _compoundRateToString;
            set
            {
                _compoundRateToString = value;
                OnPropertyChanged();
            }
        }

        //Converts interest rate into readable string
        public string InterestRateToString
        {
            get => _interestRateToString;
            set
            {
                _interestRateToString = value;
                OnPropertyChanged();
            }
        }


        public OwnedAsset(string assetName, DateTime purchaseDate, string type, float principalValue, float interestRate, float length, int compoundRate, float regularPayment, float returnGoal)
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
            UpdateOwnedAsset(); //Asset needs to be updated (return values) as soon as it is constructed.
        }

        public OwnedAsset()
        {
            AssetName = "Unknown";
            PurchaseDate = new DateTime(2019, 1, 1, 0, 0, 0);
            Type = "Unknown";
            RegularPayment = 0f;
            PrincipalValue = 0f;
            InterestRate = 0f;
            CompoundRate = 1;
            CurrentValue = 0f;
        }

        public virtual void UpdateOwnedAsset()                      
        {
            //Value doesn't grow if interest is not being applied
            if (CompoundRate == 0)
            {
                CurrentValue = PrincipalValue;
                TotalReturn = 0.0f;
                TotalReturnRate = 0.0f;
                ReturnGoalProgress = 0.0f;
            }
            else
            {
                CalculateCurrentValue();
                CalculateReturn();

                //Updates how close the return value is to reaching its return goal
                if (ReturnGoal == 0.0f)
                    ReturnGoalProgress = 0.0f;
                else
                    ReturnGoalProgress = (TotalReturn / ReturnGoal) * 100;

                if (ReturnGoalProgress <= 0)
                    ReturnGoalProgress = 0.0f;
                else if (ReturnGoalProgress >= 100)
                    ReturnGoalProgress = 100.0f;
            }

            CompoundRateConvert();
            //Changes float value (for calculation purposes) to readable percentage value
            InterestRateToString = (InterestRate * 100).ToString();
        }

        private void CompoundRateConvert()
        {
            //Converts float value (for calculation purposes) into what it means.
            switch (CompoundRate)
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

        //Calculates the Total Return of the Owned Asset
        private void CalculateReturn()
        {
            TotalReturn = CurrentValue - PrincipalValue;
            TotalReturnRate = (TotalReturn / PrincipalValue) * 100;

            //Flag for XAML code (green or red colours)
            PositiveTotal = TotalReturn > 0f;
        }

        //Uses financial calculation to calculate today's value of the asset from when it was first acquired.
        private void CalculateCurrentValue()
        {
            //Calculates days between from when asset was purchased to today
            var daysBetween = DateTime.Today - PurchaseDate;

            //Does not take regular payments into account
            var nonPaymentValue = PrincipalValue * (float)Math.Pow(1 + InterestRate / CompoundRate, daysBetween.TotalDays / 365.25 * CompoundRate);

            //Takes regular payments into account
            if (RegularPayment > 0)
                CurrentValue = (RegularPayment * (((float)Math.Pow((1 + (InterestRate / CompoundRate)), 
                    (daysBetween.TotalDays / 365.25) * CompoundRate) - 1) / (InterestRate / CompoundRate))) + nonPaymentValue;
            else
                CurrentValue = nonPaymentValue;
        }

        // Alters the asset the user is editing
        public void EditAsset(float principalValue, int compoundRate, float returnGoal, DateTime date)
        {
            if ((PrincipalValue != principalValue) && (principalValue != 0))        
                PrincipalValue = principalValue;

            if ((CompoundRate != compoundRate) && (compoundRate != -1))
                CompoundRate = compoundRate;

            if ((ReturnGoal != returnGoal) && (returnGoal != 0))
                ReturnGoal = returnGoal;

            PurchaseDate = date;

            UpdateOwnedAsset();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}