namespace WealthMate.PrivateAssets
{
    public class Portfolio
    {
        //Auto-implemented variables, from class diagram. "Owned Asset" is a separate class.
        //private List<OwnedAsset> ownedAssets;
        private float Total { get; set; }
        private float DayReturn { get; set; }
        private float TotalReturn { get; set; }
        private float ReturnGoal { get; set; }

        public Portfolio()
        {
            //ownedAssets = new List<OwnedAsset>();
            Total = 0.00f;
            DayReturn = 0.00f;
            TotalReturn = 0.00f;
            ReturnGoal = 0.00f;
        }

        /*public void AddAsset(OwnedAsset asset)
    {
        ownedAssets.Add(asset);
        Console.WriteLine("Asset successfully added");
    }

    public void RemoveAsset(OwnedAsset asset)
    {
        ownedAssets.Remove(asset);
        Console.WriteLine("Asset successfully removed");
    }*/
    }
}