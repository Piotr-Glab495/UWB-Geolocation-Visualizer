using CommunityToolkit.Mvvm.ComponentModel;
using UWB_Geolocalisation_Visualizer.Core;
using UWB_Geolocalisation_Visualizer.MVVM.ViewModel.Commands;
using UWB_Geolocalisation_Visualizer.MVVM.ViewModel.Commands.AnchorView;
using UWB_Geolocalisation_Visualizer.MVVM.ViewModel.Enums;

namespace UWB_Geolocalisation_Visualizer.MVVM.ViewModel
{
    public partial class AnchorViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string visibility = "Collapsed";

        [ObservableProperty]
        private string locationVisibility = "Collapsed";

        [ObservableProperty]
        private CoordinateViewModel xCoordinateViewModel;

        [ObservableProperty]
        private CoordinateViewModel yCoordinateViewModel;

        [ObservableProperty]
        private AnchorDialogTailViewModel anchorDialogTailViewModel;

        [ObservableProperty]
        private CommandViewModel addAnchorCommandViewModel;

        public AnchorViewModel(string displayName, TailSitesEnum tailDialogSite = TailSitesEnum.Left) : base(displayName)
        {
            xCoordinateViewModel = new CoordinateViewModel(displayName: "X:");
            yCoordinateViewModel = new CoordinateViewModel(displayName: "Y:");
            anchorDialogTailViewModel = new AnchorDialogTailViewModel(displayName: "AddingAnchor", tailDialogSite);
            addAnchorCommandViewModel = new CommandViewModel(
                    displayName: "AddingAnchor",
                    command: new AddAnchorCommand()
                );
        }
    }
}
