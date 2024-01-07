using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using UWB_Geolocation_Visualizer.Core;
using UWB_Geolocation_Visualizer.MVVM.Model;

namespace UWB_Geolocation_Visualizer.MVVM.ViewModel
{
    public partial class CoordinateViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string location;

        private readonly RegexValidator previewNumbersOnlyRegexValidator;

        public CoordinateViewModel(string displayName) : base(displayName)
        {
            previewNumbersOnlyRegexValidator = new RegexValidator("[^-.0-9]*");
            Location = "";
        }

        public CoordinateViewModel(string displayName, string location) : base(displayName)
        {
            previewNumbersOnlyRegexValidator = new RegexValidator("[^-.0-9]*");
            Location = location;
        }

        partial void OnLocationChanged(string value)
        {
            string validatedValue = previewNumbersOnlyRegexValidator.Validate(value);
            if(validatedValue != value)
            {
                this.Location = validatedValue;
            }
        }
    }
}
