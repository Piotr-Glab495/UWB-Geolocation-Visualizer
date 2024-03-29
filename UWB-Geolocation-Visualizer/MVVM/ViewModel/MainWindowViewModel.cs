﻿using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using UWB_Geolocation_Library.SimpleTypes;
using UWB_Geolocation_Visualizer.Core;
using UWB_Geolocation_Visualizer.MVVM.Model;
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

        [ObservableProperty]
        private bool isStopButtonEnabled = false;

        [ObservableProperty]
        private bool isLocaliseButtonEnabled = true;

        [ObservableProperty]
        private DataReadingModeEnum currentDataReadingMode;

        [ObservableProperty]
        private string portComNumber = "0";

        [ObservableProperty]
        private FilterTypeEnum currentFilterType;

        [ObservableProperty]
        private string filterWindowSize = "0";

        [ObservableProperty]
        private LogModeEnum currentLogMode = LogModeEnum.None;

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
                        displayName: "Wybierz tło obszaru lokalizacji",
                        command: new ChooseFileCommand(LocalizerViewModel)
                    ),
                new CommandViewModel(
                        displayName: "Zakończ",
                        command: new CloseCommand( () => { System.Windows.Application.Current.Shutdown(); } )
                    )
            };

            LocateCommand = new CommandViewModel(
                displayName: "Lokalizuj",
                command: new LocateCommand(LocalizerViewModel, this)
            );

            StopLocateCommand = new CommandViewModel(
                displayName: "Zatrzymaj lokalizację",
                command: new StopLocateCommand(this)
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

        partial void OnPortComNumberChanged(string value)
        {
            RegexValidator previewNumbersOnlyRegexValidator = new RegexValidator("[^0-9]*");
            string validatedValue = previewNumbersOnlyRegexValidator.Validate(value);
            if (validatedValue != value)
            {
                PortComNumber = validatedValue;
            }
        }

        partial void OnFilterWindowSizeChanged(string value)
        {
            RegexValidator previewNumbersOnlyRegexValidator = new RegexValidator("[^0-9]*");
            string validatedValue = previewNumbersOnlyRegexValidator.Validate(value);
            if(validatedValue != value)
            {
                FilterWindowSize = validatedValue;
            }
        }

        partial void OnCurrentLogModeChanged(LogModeEnum oldValue, LogModeEnum newValue)
        {
            //if the unchecking occured when both where checked, then service only one to be unchecked
            if (oldValue == LogModeEnum.Both)
            {
                LogModeEnum tmp = LogModeEnum.Both & newValue;
                CurrentLogMode = tmp;
                return;
            }
            //if the check of the second occured when when one was checked already, then service both to be checked
            if (oldValue != LogModeEnum.None && newValue > 0 && oldValue > 0)
            {
                LogModeEnum tmp = oldValue | newValue;
                CurrentLogMode = tmp;
                return;
            }
            //if the uncheck of the only one checked occured, then service the initial value none setting
            if(oldValue != LogModeEnum.None && oldValue != LogModeEnum.Both  && newValue < 0)
            {
                CurrentLogMode = LogModeEnum.None;
            }
        }
    }
}
