using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WealthMate.Models
{
    public class Portfolio : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private float _currentTotal;
        private float _totalReturn;
        private float _returnGoal;
        public ObservableCollection<OwnedAsset> OwnedAssets { get; }
        public float TotalReturn
        {
            get => _totalReturn;
            set
            {
                _totalReturn = value;
                OnPropertyChanged();
            }
        }
        public float PrincipalTotal { get; set; }
        public float TotalReturnRate { get; set; }
        public float ReturnGoal
        {
            get => _returnGoal;
            set
            {
                _returnGoal = value;
                OnPropertyChanged();
            }
        }
        public float ReturnGoalProgress { get; set; }
        public bool PositiveTotal { get; set; }
        public  float CurrentTotal {
            get => _currentTotal;
            set
            {
                _currentTotal = value;
                OnPropertyChanged();
            }

        }

        public Portfolio()
        {
            OwnedAssets = new ObservableCollection<OwnedAsset>();
            ReturnGoal = 0;
            UpdatePortfolio();
        }

        public Portfolio(float returnGoal)
        {
            OwnedAssets = new ObservableCollection<OwnedAsset>();
            ReturnGoal = returnGoal;
            UpdatePortfolio();
        }

        public void AddAsset(OwnedAsset asset)                      //adds asset to portfolio and instantly updates its values
        {
            OwnedAssets.Add(asset);
            UpdatePortfolio();
        }

        public void RemoveAsset(OwnedAsset asset)                   //removes asset from portfolio and instantly updates its values
        {
            OwnedAssets.Remove(asset);
            UpdatePortfolio();
        }

        public void UpdatePortfolio()
        {
            CalculateUpdatedPortfolioTotals();
            CalculateTotalReturn();

            if (ReturnGoal != 0)
                ReturnGoalProgress = (TotalReturn / ReturnGoal) * 100;       //Updates how close the return value is to reaching its return goal
        }

        private void CalculateTotalReturn()
        {
            TotalReturnRate = ((CurrentTotal - PrincipalTotal) / PrincipalTotal) * 100;

            PositiveTotal = TotalReturnRate > 0f;
        }

        private void CalculateUpdatedPortfolioTotals()
        {
            CurrentTotal = 0;                                   //sets values to zero so totals can be calculated again
            PrincipalTotal = 0;
            TotalReturn = 0;
            TotalReturnRate = 0;

            foreach (var asset in OwnedAssets)
            {
                asset.UpdateOwnedAsset();                       //Iterates through each owned asset and makes sure its updated before calculating
                CurrentTotal += asset.CurrentValue;
                PrincipalTotal += asset.PrincipalValue;
                TotalReturn += asset.TotalReturn;
            }
        }

        public void EditPortfolioGoal(float portfolioGoal)
        {
            if (portfolioGoal > 0 && portfolioGoal != ReturnGoal)
            {
                ReturnGoal = portfolioGoal;
                UpdatePortfolio();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}