using CommunityToolkit.Mvvm.ComponentModel;
using UWB_Geolocalisation_Visualizer.Core;
using UWB_Geolocalisation_Visualizer.MVVM.Model;

namespace UWB_Geolocalisation_Visualizer.MVVM.ViewModel
{
    public partial class CoordinateViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string location = "";

        private readonly RegexValidator previewNumbersOnlyRegexValidator;

        public CoordinateViewModel(string displayName) : base(displayName)
        {
            previewNumbersOnlyRegexValidator = new RegexValidator("\\P{Nd}");
        }

        partial void OnLocationChanged(string value)
        {
            string validatedValue = previewNumbersOnlyRegexValidator.Validate(value);
            if(validatedValue != value)
            {
                //We want a direct reference to prevent not necessery calls
                #pragma warning disable MVVMTK0034 // Direct field reference to [ObservableProperty] backing field
                this.location = validatedValue;
                #pragma warning restore MVVMTK0034 // Direct field reference to [ObservableProperty] backing field
            }
        }
    }
}
