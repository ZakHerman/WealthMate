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

        public void PositiveChecker()
        {
            if (ReturnPercentage > 0)
                IsPositive = true;
            else
                IsPositive = false;
        }

        public void UpdateValues(float currentValue, float principalValue)
        {
            Quantity += currentValue;
            PrincipalQuantity += principalValue;
        }

        public void CalculateReturnPercentage()
        {
            ReturnPercentage = ((Quantity - PrincipalQuantity) / PrincipalQuantity) * 100;
        }
    }
}
