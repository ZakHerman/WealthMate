using System.Diagnostics;
using System.Collections.Generic;

namespace WealthMate.Models
{
    public class Portfolio
    {
        public List<OwnedAsset> OwnedAssets { get; }

        private float _currentTotal;
        public  float CurrentTotal {
            get => _currentTotal;
            set
            {
                foreach (OwnedAsset asset in OwnedAssets)
                    _currentTotal += asset.CurrentValue;
            }
        }

        private float _totalReturn;
        public float TotalReturn {
            get => _totalReturn;
            set
            {
                foreach (OwnedAsset asset in OwnedAssets)
                    _totalReturn += asset.TotalReturn;
            }
        }

        private float _totalReturnRate;
        public float TotalReturnRate {
            get => _totalReturnRate;
            set
            {
                float _principalTotal = 0.0f;
                foreach (OwnedAsset asset in OwnedAssets)
                    _principalTotal += asset.PrincipalValue;

                _totalReturnRate = ((CurrentTotal - _principalTotal) / _principalTotal) * 100;
            }
        }

        //Sprint 2:
        //public float ReturnGoal { get; set; }

        //------------------------------------------------------------------------------------------------------------
        public Portfolio()
        {
            OwnedAssets = new List<OwnedAsset>();
            CurrentTotal = _currentTotal;
            TotalReturn = _totalReturn;
            TotalReturnRate = _totalReturnRate;
        }

        //-------------------------------------------------------------------------------------------------------------

        public void AddAsset(OwnedAsset asset)
        {
            this.OwnedAssets.Add(asset);
        }

        public void RemoveAsset(OwnedAsset asset)
        {
            this.OwnedAssets.Remove(asset);
        }
    
    }

}