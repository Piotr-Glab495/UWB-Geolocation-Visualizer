using CommunityToolkit.Mvvm.ComponentModel;
using System;
using UWB_Geolocalisation_Visualizer.Core;
using UWB_Geolocalisation_Visualizer.MVVM.ViewModel.Commands;
using UWB_Geolocalisation_Visualizer.MVVM.ViewModel.Commands.AnchorView;
using UWB_Geolocalisation_Visualizer.MVVM.ViewModel.Enums;

namespace UWB_Geolocalisation_Visualizer.MVVM.ViewModel
{
    public partial class AnchorViewModel : ViewModelBase
    {
        public int Id { get; set; }

        [ObservableProperty]
        private string visibility = "Collapsed";

        [ObservableProperty]
        private string locationVisibility = "Collapsed";

        [ObservableProperty]
        private CoordinateViewModel xCoordinateViewModel;

        [ObservableProperty]
        private CoordinateViewModel yCoordinateViewModel;

        [ObservableProperty]
        private AnchorDialogTailViewModel anchorDialogTailViewModel;

        [ObservableProperty]
        private CommandViewModel upsertAnchorCommandViewModel;

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
        }

        private void OnRequestUpsertAnchor()
        {
            LocationVisibility = "Visible";
            Visibility = "Collapsed";
            //TODO: adjust commands names and stuff in this metod
            //What to do to display an Elypse without border? Maybe a transparent bg and visibility to polygon and stack panel???
            //TODO: change name to OnRequestUpsertAnchor and do the stuff with calculating the anchorDialogTailViewModel localization
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
