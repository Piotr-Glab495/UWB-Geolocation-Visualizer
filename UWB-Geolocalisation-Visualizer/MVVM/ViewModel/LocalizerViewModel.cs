using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using UWB_Geolocalisation_Visualizer.Core;

namespace UWB_Geolocalisation_Visualizer.MVVM.ViewModel
{
    public partial class LocalizerViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<AnchorViewModel> anchors = new();

        public LocalizerViewModel(string displayName) : base(displayName) { }

        public void UpsertAnchor(AnchorViewModel anchorViewModel)
        {
            var existingAnchor = Anchors.FirstOrDefault(a => a.Equals(anchorViewModel));

            if (existingAnchor != null)
            {
                this.Update(anchorViewModel);
            }
            else
            {
                Anchors.Add(anchorViewModel);
            }
        }

        private void Update(AnchorViewModel anchorViewModel)
        {
            //TODO: Check if this works after adjusting AVM.OnRequestAddAnchor
            foreach (AnchorViewModel anchor in Anchors)
            {
                if(anchor.Equals(anchorViewModel))
                {
                    anchor.XCoordinateViewModel = anchorViewModel.YCoordinateViewModel;
                    anchor.YCoordinateViewModel = anchorViewModel.XCoordinateViewModel;
                    anchor.UpsertAnchorCommandViewModel = anchorViewModel.UpsertAnchorCommandViewModel;
                    anchor.AnchorDialogTailViewModel = anchorViewModel.AnchorDialogTailViewModel;
                    anchor.DisplayName = anchorViewModel.DisplayName;
                    anchor.LocationVisibility = anchorViewModel.LocationVisibility;
                    anchor.Visibility = anchorViewModel.Visibility;
                    break;
                }
            }
        }
    }
}
