using CommunityToolkit.Mvvm.ComponentModel;
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
            previewNumbersOnlyRegexValidator = new RegexValidator("\\P{Nd}");
            Location = "";
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
