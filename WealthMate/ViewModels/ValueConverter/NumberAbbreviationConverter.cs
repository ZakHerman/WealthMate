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
            string format;

            switch (mag)
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

            // Check number length to keep format to 3 digits long
            switch ((int)Math.Floor(Math.Log10(shortNumber)) + 1)
            {
                case 1:
                    format = "N2";
                    break;
                case 2:
                    format = "N1";
                    break;
                default:
                    format = "N0";
                    break;
            }

            return shortNumber.ToString(format) + suffix;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}