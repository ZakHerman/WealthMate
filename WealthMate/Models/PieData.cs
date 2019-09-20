using System;
using System.Collections.Generic;
using System.Text;

namespace WealthMate.Models
{
    /// <summary>
    /// Class representing the data used by the piechart 
    /// contains a Quantity and a Type
    /// </summary>
    public class PieData
    {
        public float Quantity { get; set; }
        public string AssetType { get; set; }
        public float ReturnPercentage { get; set; }
        public float PrincipalQuantity { get; set; }
        public bool IsPositive { get; set; }

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
