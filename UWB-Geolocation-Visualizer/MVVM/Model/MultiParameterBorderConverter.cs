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
            LocalizerViewModel? vm = values[1] as LocalizerViewModel;

            double worldSize, borderMin, borderMax;
            if ((string)parameter == "X")
            {
                worldSize = vm!.Width;
                if(!double.TryParse(vm!.BordersSetterViewModel.XBorderMinViewModel.Location, out borderMin))
                {
                    borderMin = 0.0;
                }
                if(!double.TryParse(vm!.BordersSetterViewModel.XBorderMaxViewModel.Location, out borderMax))
                {
                    borderMax = borderMin;
                }
            }
            else
            {
                worldSize = vm!.Height;
                if (!double.TryParse(vm!.BordersSetterViewModel.YBorderMinViewModel.Location, out borderMin))
                {
                    borderMin = 0.0;
                }
                if (!double.TryParse(vm!.BordersSetterViewModel.YBorderMaxViewModel.Location, out borderMax))
                {
                    borderMax = borderMin;
                }
            }

            if (!double.TryParse((string)values[0], out double coordinate))
            {
                coordinate = borderMin;
            }

            double size = borderMax - borderMin;
            double coordinateDisplayRatio = (coordinate - borderMin) / size;
            if(coordinateDisplayRatio < 0.0)
            {
                coordinateDisplayRatio = 0.0;
            }
            if(coordinateDisplayRatio > 1.0)
            {
                coordinateDisplayRatio = 1.0;
            }
            return coordinateDisplayRatio * worldSize;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
