using UWB_Geolocation_Visualizer.Core;

namespace UWB_Geolocation_Visualizer.MVVM.ViewModel.Commands.MainWindow
{
    public class ToggleDialogVisibilityCommand : BaseCommand
    {
        private readonly DialogViewModel dialogViewModel;
        
        public ToggleDialogVisibilityCommand(DialogViewModel dialogViewModel)
        {
            this.dialogViewModel = dialogViewModel;
        }

        public override void Execute(object? parameter)
        {
            dialogViewModel.Visibility = (dialogViewModel.Visibility == "Collapsed") ? "Visible" : "Collapsed";
        }
    }
}
