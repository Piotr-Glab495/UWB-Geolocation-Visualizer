using System;
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
            try
            {
                //TODO: add a while loop on a different thread so the data would load all the way until some button is clicked
                PointD localisedPoint = libraryFacade.Locate(localizerViewModel.AnchorsToPointDArray());
                localizerViewModel.UpsertLocalisedAnchor(localisedPoint);
            } 
            catch (Exception ex)
            {
                LocalizerViewModel.LocatingError(ex.Message);
            }
        }
    }
}
