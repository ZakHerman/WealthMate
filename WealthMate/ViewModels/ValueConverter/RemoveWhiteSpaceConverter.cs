using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace WealthMate.ViewModels.ValueConverter
{
    public class RemoveWhiteSpaceConverter : IValueConverter
    {
        // Remove all white spaces from string
        // Used for getting company names for displaying logo
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Concat(value.ToString().ToCharArray().Where(c => !char.IsWhiteSpace(c)));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}