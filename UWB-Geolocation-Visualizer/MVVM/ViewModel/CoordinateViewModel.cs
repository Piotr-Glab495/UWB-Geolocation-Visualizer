using CommunityToolkit.Mvvm.ComponentModel;
using System;
using UWB_Geolocation_Visualizer.Core;
using UWB_Geolocation_Visualizer.MVVM.Model;

namespace UWB_Geolocation_Visualizer.MVVM.ViewModel
{
    public partial class CoordinateViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string location;

        [ObservableProperty]
        private bool isEditable = true;

        private readonly RegexValidator previewNumbersOnlyRegexValidator;

        public CoordinateViewModel(string displayName) : base(displayName)
        {
            previewNumbersOnlyRegexValidator = new RegexValidator("[^-.,0-9]*");
            Location = "";
        }

        public CoordinateViewModel(string displayName, string location) : base(displayName)
        {
            previewNumbersOnlyRegexValidator = new RegexValidator("[^-.,0-9]*");
            Location = location;
        }

        public CoordinateViewModel(string displayName, double location) : base(displayName)
        {
            previewNumbersOnlyRegexValidator = new RegexValidator("[^-.,0-9]*");
            Location = FormatDouble(location);
        }

        partial void OnLocationChanged(string value)
        {
            if(value.ToLower().Contains('e'))
            {
                string[] tmp = value.Split('e');
                if(tmp.Length != 2 )
                {
                    this.Location = "0";
                    return;
                }
                double validatedFirst = double.Parse(previewNumbersOnlyRegexValidator.Validate(tmp[0]));
                double validatedSecond = double.Parse(previewNumbersOnlyRegexValidator.Validate(tmp[1]));
                this.Location = Math.Pow(validatedFirst, validatedSecond).ToString();
                return;
            }
            string validatedValue = previewNumbersOnlyRegexValidator.Validate(value);
            if(validatedValue != value)
            {
                this.Location = validatedValue;
            }
        }

        private string FormatDouble(double value)
        {
            int decimalPlaces = 10;
            string formatted = string.Format("{0:F" + decimalPlaces + "}", value);
            formatted = formatted.TrimEnd('0').TrimEnd('.').TrimEnd(',');

            return formatted;
        }
    }
}
