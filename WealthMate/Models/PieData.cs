using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WealthMate.Models
{
    // Class representing the data used by the piechart 
    // contains a Quantity and a Type
    public class PieData : INotifyPropertyChanged
    {
        private float _quantity;
        private float _returnPercentage;
        private float _principalQuantity;
        public float Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged();
            }
        }

        // Names/categorizes asset type
        public string AssetType { get; set; }
        public float ReturnPercentage
        {
            get => _returnPercentage;
            set
            {
                _returnPercentage = value;
                OnPropertyChanged();
            }
        }
        public float PrincipalQuantity
        {
            get => _principalQuantity;
            set
            {
                _principalQuantity = value;
                OnPropertyChanged();
            }
        }

        // Flag that checks if return is positive for view trigger purposes
        public bool IsPositive { get; set; }
        public int Key { get; set; }
        public string Value { get; set; }

        public PieData()
        {

        }

        public PieData(string type)
        {
            AssetType = type;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // Checks the return percentage is positive
        public void PositiveChecker()
        {
            IsPositive = ReturnPercentage > 0;
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
