using System;

public class Portfolio
{
    //Auto-implemented variables, from class diagram. "Owned Asset" is a separate class.
    private ArrayList<OwnedAsset> ownedAssets;

    private float total { get; set; }
    private float stocksDayReturn {
        get
        {
            float total = 0.00f;
            foreach (PrivateStock each in ownedAssets)
            {
                total = total + each.TotalReturn;
            }
        }
    }
    private float totalReturn {
        get
        {
            float total = 0.00f;
                foreach (Asset each in ownedAssets)
                {
                    total = total + each.TotalReturn;
                }
        }
    }
    private float returnGoal { get; set; }

	public Portfolio()
	{
        this.ownedAssets = new ArrayList<OwnedAsset>();
        this.total = 0.00f;
        this.stocksDayReturn = 0.00f;
        this.totalReturn = 0.00f;
        this.returnGoal = 0.00f;
	}

    public void addAsset(OwnedAsset asset)
    {
        ownedAssets.add(asset);
        Console.WriteLine("Asset successfully added");
    }

    public void removeAsset(Owned asset)
    {
        ownedAssets.remove(asset);
        Console.WriteLine("Asset successfully removed");
    }

    public float getTotalReturn()
    {
        
    }

}
