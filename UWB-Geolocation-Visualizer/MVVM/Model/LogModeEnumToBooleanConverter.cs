using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using UWB_Geolocation_Library.SimpleTypes;

namespace UWB_Geolocation_Visualizer.MVVM.Model
{
    public class LogModeEnumToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return false;

            return ((LogModeEnum)value & (LogModeEnum)parameter) != 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null || value is not bool)
                return DependencyProperty.UnsetValue;

            return (bool)value ? parameter : ~(LogModeEnum)parameter;
        }
    }
}
