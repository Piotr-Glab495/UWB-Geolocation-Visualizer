using CommunityToolkit.Mvvm.ComponentModel;
using System;
using UWB_Geolocation_Visualizer.MVVM.ViewModel.Commands;
using UWB_Geolocation_Visualizer.MVVM.ViewModel.Commands.AnchorView;
using UWB_Geolocation_Visualizer.MVVM.ViewModel.Commands.MainWindow;
using UWB_Geolocation_Visualizer.MVVM.ViewModel.Enums;

namespace UWB_Geolocation_Visualizer.MVVM.ViewModel
{
    public partial class AnchorViewModel : DialogViewModel
    {
        [ObservableProperty]
        private string locationVisibility = "Collapsed";

        [ObservableProperty]
        private int height = 150;

        [ObservableProperty]
        private int width = 250;

        [ObservableProperty]
        private CoordinateViewModel xCoordinateViewModel;

        [ObservableProperty]
        private CoordinateViewModel yCoordinateViewModel;

        [ObservableProperty]
        private AnchorDialogViewModel anchorDialogViewModel;

        [ObservableProperty]
        private CommandViewModel upsertAnchorCommandViewModel;

        [ObservableProperty]
        private CommandViewModel toggleDialogVisibilityCommandViewModel;

        public AnchorViewModel(
            int id,
            string displayName,
            LocalizerViewModel localizerViewModel,
            Action OnRequestUpsertAnchorAction,
            TailSitesEnum tailDialogSite = TailSitesEnum.Left
            ) : base(displayName, id)
        {
            XCoordinateViewModel = new CoordinateViewModel(displayName: "X:");
            YCoordinateViewModel = new CoordinateViewModel(displayName: "Y:");
            anchorDialogViewModel = new AnchorDialogViewModel(displayName: "AddingAnchor", tailDialogSite);
            UpsertAnchorCommand upsertAnchorCommand = new(
                    anchorViewModel: this,
                    localizerViewModel: localizerViewModel,
                    requestAddAnchorDelegates: new Action[] { OnRequestUpsertAnchorAction, OnRequestUpsertAnchor }
                );
            upsertAnchorCommandViewModel = new CommandViewModel(
                    displayName: "Dodaj " + (id + 1).ToString() + " kotwicę",
                    command: upsertAnchorCommand
                );
            toggleDialogVisibilityCommandViewModel = new CommandViewModel(
                    displayName: "Edytuj kotwicę",
                    command: new ToggleDialogVisibilityCommand(this)
                );
        }

        private void OnRequestUpsertAnchor()
        {
            LocationVisibility = "Visible";
            Visibility = "Collapsed";
            AnchorDialogViewModel.DisplayName = "Kotwica " + (Id + 1).ToString();
            UpsertAnchorCommandViewModel.DisplayName = "Edytuj " + (Id + 1).ToString() + " kotwicę";
            DisplayName = "Podaj nowe położenie kotwicy";
        }
    }
}
