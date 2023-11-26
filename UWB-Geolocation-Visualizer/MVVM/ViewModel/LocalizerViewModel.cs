using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using UWB_Geolocation_Visualizer.Core;

namespace UWB_Geolocation_Visualizer.MVVM.ViewModel
{
    public partial class LocalizerViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<AnchorViewModel> anchors = new();

        [ObservableProperty]
        private int width = 720;

        [ObservableProperty]
        private int height = 600;

        public LocalizerViewModel(string displayName) : base(displayName) { }

        public void UpsertAnchor(AnchorViewModel anchorViewModel)
        {
            Prepare(anchorViewModel);
            var existingAnchor = Anchors.FirstOrDefault(a => a.Equals(anchorViewModel));

            if (existingAnchor != null)
            {
                Update(anchorViewModel);
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

        private void Prepare(AnchorViewModel anchorViewModel)
        {
            int x = int.Parse(anchorViewModel.XCoordinateViewModel.Location);
            int y = int.Parse(anchorViewModel.YCoordinateViewModel.Location);

            if (x <= 0.5 * anchorViewModel.Width)
            {
                if (y <= 0.5 * anchorViewModel.Height)
                {
                    anchorViewModel.AnchorDialogTailViewModel.DialogSite = Enums.TailSitesEnum.LeftBottomCorner;
                    return;
                }

                if (y >= (this.Height - 0.5 * anchorViewModel.Height))
                {
                    anchorViewModel.AnchorDialogTailViewModel.DialogSite = Enums.TailSitesEnum.LeftTopCorner;
                    return;
                }

                anchorViewModel.AnchorDialogTailViewModel.DialogSite = Enums.TailSitesEnum.Left;
                return;
            }

            if (x >= (this.Width - 0.5 * anchorViewModel.Width))
            {
                if (y <= 0.5 * anchorViewModel.Height)
                {
                    anchorViewModel.AnchorDialogTailViewModel.DialogSite = Enums.TailSitesEnum.RightBottomCorner;
                    return;
                }

                if (y >= (this.Height - 0.5 * anchorViewModel.Height))
                {
                    anchorViewModel.AnchorDialogTailViewModel.DialogSite = Enums.TailSitesEnum.RightTopCorner;
                    return;
                }

                anchorViewModel.AnchorDialogTailViewModel.DialogSite = Enums.TailSitesEnum.Right;
                return;
            }

            if (y <= 0.5 * anchorViewModel.Height)
            {
                anchorViewModel.AnchorDialogTailViewModel.DialogSite = Enums.TailSitesEnum.Top;
                return;
            }

            if (y >= (this.Height - 0.5 * anchorViewModel.Height))
            {
                anchorViewModel.AnchorDialogTailViewModel.DialogSite = Enums.TailSitesEnum.Bottom;
                return;
            }

            anchorViewModel.AnchorDialogTailViewModel.DialogSite = Enums.TailSitesEnum.Left;
        }
    }
}
