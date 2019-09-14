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
    }
}
