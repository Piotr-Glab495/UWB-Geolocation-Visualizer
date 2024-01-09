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
            libraryFacade = new GeolocationLibraryFacade(DataReadingModeEnum.TestMode);
        }

        public override void Execute(object? parameter)
        {
            try
            {
                libraryFacade.Locate(localizerViewModel.AnchorsToPointDArray());
            } 
            catch (Exception ex)
            {
                LocalizerViewModel.LocatingError(ex.Message);
            }
        }
    }
}
