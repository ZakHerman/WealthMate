using System.Diagnostics;
using System.Collections.Generic;

namespace WealthMate.PrivateAssets
{
    public class Portfolio
    {
        private List<OwnedAsset> OwnedAssets;
        public float CurrentTotal {
            get
            {
                float total = 0.0f;
                foreach (OwnedAsset asset in OwnedAssets)
                {
                    total =+ asset.CurrentValue;
                }
                return total;
            }
        }
        public float DayReturn {
        get
            {   //Need to confirm if "OwnedStock" can be used & if the "if" statement is req.
                float dayTotal = 0.0f;
                foreach(OwnedStock stock in OwnedAssets) {

                    if (stock is OwnedStock) {
                        dayTotal =+ stock.DayReturn;
                    }
                }
                return dayTotal;
            }
        }
        public float TotalReturn {
            get
            {
                float returns = 0.0f;
                foreach (OwnedAsset asset in OwnedAssets)
                {
                        returns =+ asset.TotalReturn;
                }
                return returns;
            }
        }
        public float ReturnGoal { get; set; }

        public Portfolio()
        {
            OwnedAssets = new List<OwnedAsset>();
            ReturnGoal = 0.00f;
        }

        public void AddAsset(OwnedAsset asset)
        {
            this.OwnedAssets.Add(asset);
            Debug.WriteLine("Asset successfully added");
        }

        public void RemoveAsset(OwnedAsset asset)
        {
            this.OwnedAssets.Remove(asset);
            Debug.WriteLine("Asset successfully removed");
        }
    }
}