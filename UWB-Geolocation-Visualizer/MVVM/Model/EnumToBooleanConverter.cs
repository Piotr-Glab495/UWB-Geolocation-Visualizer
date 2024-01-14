using System;
using System.Globalization;
using System.Windows.Data;

namespace UWB_Geolocation_Visualizer.MVVM.Model
{
    public class EnumToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return false;

            return value.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null || value is not bool)
                return false;

            return (bool)value ? parameter : false;
        }
    }
}
