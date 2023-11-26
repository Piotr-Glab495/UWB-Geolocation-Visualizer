using CommunityToolkit.Mvvm.ComponentModel;
using System;
using UWB_Geolocation_Visualizer.Core;
using UWB_Geolocation_Visualizer.MVVM.ViewModel.Commands;
using UWB_Geolocation_Visualizer.MVVM.ViewModel.Commands.AnchorView;
using UWB_Geolocation_Visualizer.MVVM.ViewModel.Commands.MainWindow;
using UWB_Geolocation_Visualizer.MVVM.ViewModel.Enums;

namespace UWB_Geolocation_Visualizer.MVVM.ViewModel
{
    public partial class AnchorViewModel : ViewModelBase
    {
        public int Id { get; set; }

        [ObservableProperty]
        private string visibility = "Collapsed";

        [ObservableProperty]
        private string borderBackground = "Transparent";

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
        private AnchorDialogTailViewModel anchorDialogTailViewModel;

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
            ) : base(displayName)
        {
            Id = id;
            XCoordinateViewModel = new CoordinateViewModel(displayName: "X:");
            YCoordinateViewModel = new CoordinateViewModel(displayName: "Y:");
            anchorDialogTailViewModel = new AnchorDialogTailViewModel(displayName: "AddingAnchor", tailDialogSite);
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
                    command: new ToggleAnchorDialogVisibilityCommand(this)
                );
        }

        private void OnRequestUpsertAnchor()
        {
            LocationVisibility = "Visible";
            Visibility = "Collapsed";
            AnchorDialogTailViewModel.DisplayName = "Kotwica " + (Id + 1).ToString();
            UpsertAnchorCommandViewModel.DisplayName = "Edytuj " + (Id + 1).ToString() + " kotwicę";
        }

        partial void OnVisibilityChanged(string value)
        {
            BorderBackground = (value == "Visible" ? "#3d8693" : "Transparent");
        }

        public bool Equals(AnchorViewModel? other)
        {
            if (other is null)
                return false;

            return Id == other.Id;
        }

        public override bool Equals(object? obj) => Equals(obj as AnchorViewModel);

        public override int GetHashCode() => Id.GetHashCode();
    }
}
