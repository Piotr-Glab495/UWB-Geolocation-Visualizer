using System;
using System.Threading.Tasks;
using System.Windows.Threading;
using UWB_Geolocation_Library;
using UWB_Geolocation_Library.SimpleTypes;
using UWB_Geolocation_Visualizer.Core;

namespace UWB_Geolocation_Visualizer.MVVM.ViewModel.Commands.MainWindow
{
    public class LocateCommand : CommandBase
    {
        private readonly LocalizerViewModel localizerViewModel;
        private readonly MainWindowViewModel mainWindowViewModel;
        private readonly GeolocationLibraryFacade libraryFacade;

        public LocateCommand(LocalizerViewModel localizerViewModel, MainWindowViewModel mainWindowViewModel)
        {
            this.localizerViewModel = localizerViewModel;
            this.mainWindowViewModel = mainWindowViewModel;
            libraryFacade = new GeolocationLibraryFacade();
        }

        public override void Execute(object? parameter)
        {
            mainWindowViewModel.IsLocaliseButtonEnabled = false;
            mainWindowViewModel.IsStopButtonEnabled = true;
            Dispatcher d = Dispatcher.CurrentDispatcher;
            _ = Task.Run(() =>
            {
                try
                {
                    while(mainWindowViewModel.IsStopButtonEnabled)
                    {
                        GeolocationLibraryDTO libraryDTO = new(
                            localizerViewModel.AnchorsToPointDArray(),
                            mainWindowViewModel.CurrentDataReadingMode,
                            int.Parse(mainWindowViewModel.PortComNumber),
                            mainWindowViewModel.CurrentLogMode,
                            mainWindowViewModel.CurrentFilterType,
                            int.Parse(mainWindowViewModel.FilterWindowSize)
                        );
                        PointD localisedPoint = libraryFacade.Locate(libraryDTO);
                        d.Invoke(() => {
                            localizerViewModel.UpsertLocalisedAnchor(localisedPoint);
                        });
                    }

                    libraryFacade.ClosePort();
                    libraryFacade.DisposeLogger();
                } 
                catch (Exception ex)
                {
                    d.Invoke(() => {
                        LocalizerViewModel.LocatingError(ex.Message);
                        mainWindowViewModel.IsStopButtonEnabled = false;
                        mainWindowViewModel.IsLocaliseButtonEnabled = true;
                    });
                } 
                
            });
            
        }
    }
}
