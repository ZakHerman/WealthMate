namespace WealthMate.Models
{
    /// <summary>
    /// Class representing the data used by the piechart 
    /// contains a Quantity and a Type
    /// </summary>
    public class PieData
    {
        public float Quantity { get; set; }                 //Total current value of PieData category
        public string AssetType { get; set; }               //Names/categorizes asset type
        public float ReturnPercentage { get; set; }         //Returns percentage of PieChart asset type category
        public float PrincipalQuantity { get; set; }        //Total principal value of PieData category
        public bool IsPositive { get; set; }                //Flag that checks if return is positive for view trigger purposes

        public PieData(string type)
        {
            AssetType = type;
        }

        // Checks the return percentage is positive
        public void PositiveChecker()
        {
            if (ReturnPercentage > 0)
                IsPositive = true;
            else
                IsPositive = false;
        }

        // Updates the values for the pie chart title
        public void UpdateValues(float currentValue, float principalValue)
        {
            Quantity += currentValue;
            PrincipalQuantity += principalValue;
        }

        // Calculates return percentage
        public void CalculateReturnPercentage()
        {
            ReturnPercentage = ((Quantity - PrincipalQuantity) / PrincipalQuantity) * 100;
        }
    }
}
