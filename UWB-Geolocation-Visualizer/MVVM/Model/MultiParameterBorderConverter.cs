using System;
using System.Globalization;
using System.Windows.Data;
using UWB_Geolocation_Visualizer.MVVM.ViewModel;

namespace UWB_Geolocation_Visualizer.MVVM.Model
{
    public class MultiParameterBorderConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double coordinate = double.Parse((string)values[0]);
            LocalizerViewModel? vm = values[1] as LocalizerViewModel;

            double worldSize, borderMin, borderMax;
            if ((string)parameter == "X")
            {
                worldSize = vm!.Width;
                borderMin = vm!.XBorderMin;
                borderMax = vm!.XBorderMax;
            }
            else
            {
                worldSize = vm!.Height;
                borderMin = vm!.YBorderMin;
                borderMax = vm!.YBorderMax;
            }

            double size = borderMax - borderMin;
            double coordinateDisplayRatio = (coordinate - borderMin) / size;
            return coordinateDisplayRatio * worldSize;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
