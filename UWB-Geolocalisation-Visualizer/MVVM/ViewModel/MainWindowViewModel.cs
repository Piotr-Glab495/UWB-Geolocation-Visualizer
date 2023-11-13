using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using UWB_Geolocalisation_Visualizer.Core;
using UWB_Geolocalisation_Visualizer.MVVM.ViewModel.Commands;
using UWB_Geolocalisation_Visualizer.MVVM.ViewModel.Commands.MainWindow;

namespace UWB_Geolocalisation_Visualizer.MVVM.ViewModel
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        /**
         * <summary>
         * The property for a ViewModel servicing the current view.
         * It is here for future purposes e.g. adding new views which would be bound to other ViewModels and switched with RelayCommands
         * </summary>
         */
        private readonly LocalizerViewModel localizerViewModel;

        /**
         *<summary>
         * The ObservableProperty for a ViewModel servicing a dialog used for adding the new anchor
         *</summary>
         */
        [ObservableProperty]
        private AnchorViewModel anchorViewModel;

        /**
         *<summary>
         * The ObservableProperty for a ViewModel servicing a currentlly displayed view.
         * It will be switched to other ViewModels if some new views would be added
         *</summary>
         */
        [ObservableProperty]
        private object currentView;

        /**
         *<summary>
         * Observable collection of CommandViewModels to be displayed on the left side of the MainWindow as clickable objects
         *</summary>
         */
        [ObservableProperty]
        private ObservableCollection<CommandViewModel> commands;

        public MainWindowViewModel() : base(displayName: "UWB Geolocalisation Visualizer")
        {
            localizerViewModel = new LocalizerViewModel(displayName: "Lokalizator");
            anchorViewModel = new AnchorViewModel(displayName: "Podaj położenie kotwicy");
            CurrentView = localizerViewModel;
            this.Commands = new ObservableCollection<CommandViewModel>
            {
                new CommandViewModel(
                        displayName: "Dodaj kotwicę",
                        command: new ToggleAnchorDialogVisibilityCommand(anchorViewModel)
                    ),
                new CommandViewModel(
                        displayName: "Zakończ",
                        command: new CloseCommand( () => { System.Windows.Application.Current.Shutdown(); } )
                    )
            };
        }
    }
}
