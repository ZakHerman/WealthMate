using System;
using System.Globalization;
using Xamarin.Forms;

namespace WealthMate.ViewModels.ValueConverter
{
    public class NumberAbbreviationConverter : IValueConverter
    {
        // Convert to currency format else return N/A
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            long.TryParse(value.ToString(), out var val);

            // If unable to parse value return 0
            if (val <= 0)
                return 0.ToString("N0");

            var mag = (long)(Math.Floor(Math.Log10(val)) / 3);
            var divisor = Math.Pow(10, mag * 3);
            var shortNumber = val / divisor;
            var suffix = string.Empty;

            switch(mag)
            {
                case 0:
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
            }

            var format = parameter.ToString() == "Int" && mag <= 1 ? "N0" : "N2";
            return shortNumber.ToString(format) + suffix;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}