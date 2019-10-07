using System;
using System.Globalization;
using Xamarin.Forms;

namespace WealthMate.ViewModels.ValueConverter
{
    public class LogConverter : IValueConverter
    {
        // Convert to currency format else return N/A
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            long.TryParse(value.ToString(), out var val);
            var mag = (long)(Math.Floor(Math.Log10(val)) / 3);
            var divisor = Math.Pow(10, mag * 3);
            var shortNumber = val / divisor;

            string suffix;

            switch(mag)
            {
                case 0:
                    suffix = string.Empty;
                    break;
                case 1:
                    suffix = "K";
                    break;
                case 2:
                    suffix = "M";
                    break;
                case 3:
                    suffix = "B";
                    break;
                default:
                    suffix = string.Empty;
                    break;
            }

            return shortNumber.ToString("N2") + suffix;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


    }
}