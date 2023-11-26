using UWB_Geolocation_Visualizer.Core;

namespace UWB_Geolocation_Visualizer.MVVM.ViewModel.Commands.MainWindow
{
    public class ToggleAnchorDialogVisibilityCommand : BaseCommand
    {
        private readonly AnchorViewModel anchorViewModel;
        
        public ToggleAnchorDialogVisibilityCommand(AnchorViewModel anchorViewModel)
        {
            this.anchorViewModel = anchorViewModel;
        }

        public override void Execute(object? parameter)
        {
            anchorViewModel.Visibility = (anchorViewModel.Visibility == "Collapsed") ? "Visible" : "Collapsed";
        }
    }
}
