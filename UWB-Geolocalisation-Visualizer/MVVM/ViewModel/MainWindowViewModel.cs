using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
         * The ObservableProperty for a ViewModel servicing a currentlly displayed view.
         * It will be switched to other ViewModels if some new views would be added
         *</summary>
         */
        [ObservableProperty]
        private object _currentView;

        /**
         *<summary>
         * Observable collection of CommandViewModels to be displayed on the left side of the MainWindow as clickable objects
         *</summary>
         */
        [ObservableProperty]
        private ObservableCollection<CommandViewModel> commands;

        public MainWindowViewModel() : base(displayName: "UWB Geolocalisation Visualizer")
        {
            this.localizerViewModel = new LocalizerViewModel(displayName: "Lokalizator");
            CurrentView = localizerViewModel;
            this.Commands = new ObservableCollection<CommandViewModel>
            {
                new CommandViewModel(
                        displayName: "Dodaj kotwicę",
                        command: new RelayCommand(delegate { }) //TODO: change the empty delegate to an appropriate command adding an anhor
                    ),
                new CommandViewModel(
                        displayName: "Zakończ",
                        command: new CloseCommand()
                    )
            };
            //Setting the CloseCommand.RequestClose effect - that is giving it the opportunity to close the application.
            (this.Commands[1].Command as CloseCommand)!.RequestClose += delegate { System.Windows.Application.Current.Shutdown(); };
        }
    }
}
