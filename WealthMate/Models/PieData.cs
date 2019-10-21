using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WealthMate.Models
{
    /// <summary>
    /// Class representing the data used by the piechart 
    /// contains a Quantity and a Type
    /// </summary>
    public class PieData : INotifyPropertyChanged
    {
        private float _quantity;
        private float _returnPercentage;
        private float _principalQuantity;
        public float Quantity                //Total current value of PieData category
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged();
            }
        }
        public string AssetType { get; set; }               //Names/categorizes asset type
        public float ReturnPercentage       //Returns percentage of PieChart asset type category
        {
            get => _returnPercentage;
            set
            {
                _returnPercentage = value;
                OnPropertyChanged();
            }
        }
        public float PrincipalQuantity       //Total principal value of PieData category
        {
            get => _principalQuantity;
            set
            {
                _principalQuantity = value;
                OnPropertyChanged();
            }
        }
        public bool IsPositive { get; set; }                //Flag that checks if return is positive for view trigger purposes
        public int Key { get; set; }
        public string Value { get; set; }

        public PieData(string type)
        {
            AssetType = type;
        }

        public PieData()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

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

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
