using CommunityToolkit.Mvvm.ComponentModel;
using UWB_Geolocalisation_Visualizer.Core;
using UWB_Geolocalisation_Visualizer.MVVM.ViewModel.Commands;
using UWB_Geolocalisation_Visualizer.MVVM.ViewModel.Commands.Anchor;

namespace UWB_Geolocalisation_Visualizer.MVVM.ViewModel
{
    public partial class CoordinateViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string location = "";

        [ObservableProperty]
        private CommandViewModel previewNumbersOnlyCommandViewModel;

        public CoordinateViewModel(string displayName) : base(displayName)
        {
            PreviewNumbersOnlyCommandViewModel = 
                new CommandViewModel(
                    displayName: "previewNumbersOnlyCommand",
                    new RegexValidationCommand("\\P{Nd}")
                );
        }
    }
}
