using UWB_Geolocation_Visualizer.Core;

namespace UWB_Geolocation_Visualizer.MVVM.ViewModel.Commands.MainWindow
{
    public class StopLocateCommand : BaseCommand
    {
        private readonly LocalizerViewModel localizerViewModel;

        public StopLocateCommand(LocalizerViewModel localizerViewModel)
        {
            this.localizerViewModel = localizerViewModel;
        }

        public override void Execute(object? parameter)
        {
            localizerViewModel.IsStopButtonEnabled = false;
            localizerViewModel.IsLocaliseButtonEnabled = true;
        }
    }
}
