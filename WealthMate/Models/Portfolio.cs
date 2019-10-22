using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace WealthMate.Models
{
    public class Portfolio : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private float _currentTotal;
        private float _totalReturn;
        private float _totalReturnRate;
        private float _returnGoal;
        private float _returnGoalProgress;

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
        public bool PositiveTotal { get; set; }
        public float TotalReturnRate
        {
            get => _totalReturnRate;
            set
            {
                _totalReturnRate = value;
                OnPropertyChanged();
            }
        }
        public float ReturnGoal
        {
            get => _returnGoal;
            set
            {
                _returnGoal = value;
                OnPropertyChanged();
            }
        }
        public float ReturnGoalProgress
        {
            get => _returnGoalProgress;
            set
            {
                _returnGoalProgress = value;
                OnPropertyChanged();
            }
        }
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

        // Adds asset to portfolio and instantly updates its values
        public async void AddAsset(OwnedAsset asset)
        {
            OwnedAssets.Add(asset);

            if (asset.GetType() == typeof(OwnedStock))
                await App.Database.SaveOwnedStockAsync((OwnedStock)asset);
            else
            {
                await App.Database.SaveOwnedAssetAsync(asset);
                var test = await App.Database.GetOwnedAssetsAsync();
                Debug.WriteLine("Number: " + test.Count);
            }

            UpdatePortfolio();
        }

        // Removes asset from portfolio and instantly updates its values
        public void RemoveAsset(OwnedAsset asset)
        {
            OwnedAssets.Remove(asset);
            UpdatePortfolio();
        }

        public void UpdatePortfolio()
        {
            CalculateUpdatedPortfolioTotals();
            CalculateTotalReturn();

            // Updates how close the return value is to reaching its return goal
            if (ReturnGoal != 0)
            {
                ReturnGoalProgress = (TotalReturn / ReturnGoal) * 100;

                if (ReturnGoalProgress <= 0)
                    ReturnGoalProgress = 0.0f;
                else if (ReturnGoalProgress >= 100)
                    ReturnGoalProgress = 100.0f;
            }  
        }

        private void CalculateTotalReturn()
        {
            if (PrincipalTotal == 0.0f)
                TotalReturnRate = 0.0f;
            else
                TotalReturnRate = (TotalReturn / PrincipalTotal) * 100;

            PositiveTotal = TotalReturnRate > 0f;
        }

        private void CalculateUpdatedPortfolioTotals()
        {
            // Sets values to zero so totals can be calculated again
            CurrentTotal = 0;
            PrincipalTotal = 0;
            TotalReturn = 0;

            // Iterates through each owned asset and makes sure its updated before calculating
            foreach (var asset in OwnedAssets)
            {
                asset.UpdateOwnedAsset();
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