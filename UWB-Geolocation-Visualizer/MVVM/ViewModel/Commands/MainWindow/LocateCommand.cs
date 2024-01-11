using System;
using System.Threading.Tasks;
using System.Windows.Threading;
using UWB_Geolocation_Library;
using UWB_Geolocation_Library.SimpleTypes;
using UWB_Geolocation_Visualizer.Core;

namespace UWB_Geolocation_Visualizer.MVVM.ViewModel.Commands.MainWindow
{
    public class LocateCommand : BaseCommand
    {
        private readonly LocalizerViewModel localizerViewModel;
        private readonly GeolocationLibraryFacade libraryFacade;

        public LocateCommand(LocalizerViewModel localizerViewModel)
        {
            this.localizerViewModel = localizerViewModel;
            libraryFacade = new GeolocationLibraryFacade();
        }

        public override void Execute(object? parameter)
        {
            localizerViewModel.IsLocaliseButtonEnabled = false;
            localizerViewModel.IsStopButtonEnabled = true;
            Dispatcher d = Dispatcher.CurrentDispatcher;
            _ = Task.Run(() =>
            {
                try
                {
                    while(localizerViewModel.IsStopButtonEnabled)
                    {
                        PointD localisedPoint = libraryFacade.Locate(localizerViewModel.AnchorsToPointDArray());
                        d.Invoke(() => {
                            localizerViewModel.UpsertLocalisedAnchor(localisedPoint);
                        });
                    }
                } 
                catch (Exception ex)
                {
                    libraryFacade.ClosePort();
                    d.Invoke(() => {
                        LocalizerViewModel.LocatingError(ex.Message);
                        localizerViewModel.IsStopButtonEnabled = false;
                        localizerViewModel.IsLocaliseButtonEnabled = true;
                    });
                } 
                
            });
            
        }
    }
}
