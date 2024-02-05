using UWB_Geolocation_Visualizer.Core;

namespace UWB_Geolocation_Visualizer.MVVM.ViewModel.Commands.MainWindow
{
    public class StopLocateCommand : CommandBase
    {
        private readonly MainWindowViewModel mainWindowViewModel;

        public StopLocateCommand(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
        }

        public override void Execute(object? parameter)
        {
            mainWindowViewModel.IsStopButtonEnabled = false;
            mainWindowViewModel.IsLocaliseButtonEnabled = true;
        }
    }
}
