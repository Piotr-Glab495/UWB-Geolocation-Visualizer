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
                //TODO: add a while loop so the data would load all the way until some button is clicked
            _ = Task.Run(async () =>
            {
                Dispatcher d = Dispatcher.CurrentDispatcher;
                try
                {
                    PointD localisedPoint = await libraryFacade.Locate(localizerViewModel.AnchorsToPointDArray());
                    d.Invoke(() => {
                        localizerViewModel.UpsertLocalisedAnchor(localisedPoint);    //ehh after adding data readers with awaits and async reading this UI isn't updating even from inside of the dispatcher
                    });
                } 
                catch (Exception ex)
                {
                    d.Invoke(() => { LocalizerViewModel.LocatingError(ex.Message); });  //And this one is ok
                } 
                
            });
            
        }
    }
}
