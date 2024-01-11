using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using UWB_Geolocation_Visualizer.Core;
using UWB_Geolocation_Visualizer.MVVM.ViewModel.Commands;
using UWB_Geolocation_Visualizer.MVVM.ViewModel.Commands.MainWindow;

namespace UWB_Geolocation_Visualizer.MVVM.ViewModel
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        /**
         * <summary>
         * The property for a ViewModel servicing the current view.
         * It is here for future purposes e.g. adding new views which would be bound to other ViewModels and switched with RelayCommands
         * </summary>
         */
        [ObservableProperty]
        private LocalizerViewModel localizerViewModel;

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

        /**
         *<summary>
         * Observable CommandViewModel to be displayed on the left side of the MainWindow as clickable object
         *</summary>
         */
        [ObservableProperty]
        private CommandViewModel locateCommand;

        /**
         *<summary>
         * Observable CommandViewModel to be displayed on the left side of the MainWindow as clickable object
         *</summary>
         */
        [ObservableProperty]
        private CommandViewModel stopLocateCommand;

        public MainWindowViewModel() : base(displayName: "UWB Geolocation Visualizer")
        {
            localizerViewModel = new LocalizerViewModel(displayName: "Lokalizator");
            anchorViewModel = new AnchorViewModel(
                    id: LocalizerViewModel.Anchors.Count,
                    displayName: "Podaj położenie kotwicy",
                    localizerViewModel: LocalizerViewModel,
                    OnRequestUpsertAnchorAction: ReallocateAnchorViewModel
                );

            Commands = new ObservableCollection<CommandViewModel>
            {
                new CommandViewModel(
                        displayName: "Dodaj kotwicę",
                        command: new ToggleDialogVisibilityCommand(anchorViewModel)
                    ),
                new CommandViewModel(
                        displayName: "Edytuj obszar lokalizacji",
                        command: new ToggleDialogVisibilityCommand(LocalizerViewModel.BordersSetterViewModel)
                    ),
                new CommandViewModel(
                        displayName: "Zakończ",
                        command: new CloseCommand( () => { System.Windows.Application.Current.Shutdown(); } )
                    )
            };

            LocateCommand = new CommandViewModel(
                displayName: "Lokalizuj",
                command: new LocateCommand(LocalizerViewModel)
            );

            StopLocateCommand = new CommandViewModel(
                displayName: "Zatrzymaj lokalizację",
                command: new StopLocateCommand(LocalizerViewModel)
            );
            CurrentView = localizerViewModel;
        }

        /**
         *<summary>
         * This method is used to create a new instance of AnchorViewModel for adding anchors OnRequestUpsertAnchor
         * when the existing AVM is becoming an existing and displayed Anchor.
         *</summary>
         */
        private void ReallocateAnchorViewModel()
        {
            AnchorViewModel = new AnchorViewModel(
                    id: LocalizerViewModel.Anchors.Count,
                    displayName: "Podaj położenie kotwicy",
                    localizerViewModel: LocalizerViewModel,
                    OnRequestUpsertAnchorAction: ReallocateAnchorViewModel
                );
            CommandViewModel tmp = new(
                        displayName: "Dodaj kotwicę",
                        command: new ToggleDialogVisibilityCommand(AnchorViewModel)
                    );
            foreach ( CommandViewModel command in Commands )
            {
                if(command.Equals(tmp))
                {
                    command.Command = tmp.Command;  //binding the command with new AVM
                    break;
                }
            }
        }
    }
}
