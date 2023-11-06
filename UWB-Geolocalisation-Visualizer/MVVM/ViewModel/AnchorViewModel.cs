using CommunityToolkit.Mvvm.ComponentModel;
using UWB_Geolocalisation_Visualizer.Core;

namespace UWB_Geolocalisation_Visualizer.MVVM.ViewModel
{
    public partial class AnchorViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string visibility = "Collapsed";

        [ObservableProperty]
        private int anchorZIndex = 1000;

        [ObservableProperty]
        private CoordinateViewModel xCoordinateViewModel;

        [ObservableProperty]
        private CoordinateViewModel yCoordinateViewModel;

        public AnchorViewModel(string displayName) : base(displayName)
        {
            xCoordinateViewModel = new CoordinateViewModel(displayName: "X:");
            yCoordinateViewModel = new CoordinateViewModel(displayName: "Y:");
        }
    }
}
