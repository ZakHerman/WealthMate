using System.Diagnostics;

namespace WealthMate.PrivateAssets
{
    public class Portfolio
    {
        //Auto-implemented variables, from class diagram. "Owned Asset" is a separate class.
        private OwnedAsset[] OwnedAssets = new OwnedAsset[100];
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
            //Max of 100 assets in the portfolio?
            OwnedAssets = new OwnedAsset[100];
            ReturnGoal = 0.00f;
        }

        public void addAsset(OwnedAsset asset)
        {
            int i = 0;
            while(this.OwnedAssets[i] != null && i < this.OwnedAssets.Length) {
                i++;
            }

            if(this.OwnedAssets[i] != null) {
                Debug.WriteLine("Asset list is full.");
            }                        
            else {
                this.OwnedAssets[i] = asset;
                Debug.WriteLine("Asset successfully added");
            }
        }

        public void removeAsset(OwnedAsset asset)
        {
            bool removed = false;

            for(int i = 0; i < this.OwnedAssets.Length; i++) {
                    if(OwnedAssets[i] == asset) {
                        OwnedAssets[i] = null;
                        removed = true;
                    }                                           
            }
            if(removed == true) {
                Debug.WriteLine("Asset successfully removed");
            }
            else {
                    Debug.WriteLine("Asset not found");
            }            
        }
    }
}