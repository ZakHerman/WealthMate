using System;
using System.Globalization;
using Xamarin.Forms;

namespace WealthMate.ViewModels.ValueConverter
{
    public class UtcToLocalDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime time)
                return time.ToLocalTime().ToString("dd MMM yyyy HH:mm");

            DateTime.TryParse(value?.ToString(), out time);

            return time.ToLocalTime().ToString("dd MMM yyyy HH:mm");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}