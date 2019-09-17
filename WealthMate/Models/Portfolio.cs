using System.Diagnostics;
using System.Collections.Generic;

namespace WealthMate.Models
{
    public class Portfolio
    {
        public List<OwnedAsset> OwnedAssets { get; }
        public  float CurrentTotal { get; set; }
        public float TotalReturn { get; set; }
        public float PrincipalTotal { get; set; }
        public float TotalReturnRate { get; set; }
        public bool PositiveTotal { get; set; }

        //Sprint 2:
        //public float ReturnGoal { get; set; }

        //------------------------------------------------------------------------------------------------------------
        public Portfolio()
        {
            OwnedAssets = new List<OwnedAsset>();
            UpdatePortfolio();
        }

        //-------------------------------------------------------------------------------------------------------------

        public void AddAsset(OwnedAsset asset)
        {
            this.OwnedAssets.Add(asset);
            UpdatePortfolio();
        }

        public void RemoveAsset(OwnedAsset asset)
        {
            this.OwnedAssets.Remove(asset);
            UpdatePortfolio();
        }

        public void UpdatePortfolio()
        {
            CurrentTotal = 0;
            PrincipalTotal = 0;
            TotalReturn = 0;
            TotalReturnRate = 0;

            foreach (OwnedAsset asset in OwnedAssets)
            {
                asset.UpdateOwnedAsset();
                CurrentTotal += asset.CurrentValue;
                PrincipalTotal += asset.PrincipalValue;
                TotalReturn += asset.TotalReturn;
            }
            TotalReturnRate = ((CurrentTotal - PrincipalTotal) / PrincipalTotal) * 100;

            if (TotalReturn > 0f)
                PositiveTotal = true;
            else
                PositiveTotal = false;
        }
    
    }

}