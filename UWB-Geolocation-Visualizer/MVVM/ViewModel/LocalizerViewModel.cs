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

        [ObservableProperty]
        private int xBorderMin = -360;

        [ObservableProperty]
        private int xBorderMax = 360;

        [ObservableProperty]
        private int yBorderMin = -300;

        [ObservableProperty]
        private int yBorderMax = 300;

        public LocalizerViewModel(string displayName) : base(displayName) { }

        public void UpsertAnchor(AnchorViewModel anchorViewModel)
        {
            PrepareAnchorForDisplaying(anchorViewModel);
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
            foreach (AnchorViewModel anchor in Anchors)
            {
                if(anchor.Equals(anchorViewModel))
                {
                    anchor.XCoordinateViewModel = anchorViewModel.XCoordinateViewModel;
                    anchor.YCoordinateViewModel = anchorViewModel.YCoordinateViewModel;
                    anchor.UpsertAnchorCommandViewModel = anchorViewModel.UpsertAnchorCommandViewModel;
                    anchor.AnchorDialogViewModel = anchorViewModel.AnchorDialogViewModel;
                    anchor.DisplayName = anchorViewModel.DisplayName;
                    anchor.LocationVisibility = anchorViewModel.LocationVisibility;
                    anchor.Visibility = anchorViewModel.Visibility;
                    break;
                }
            }
        }

        //TODO: Add a messenger and use it to react to the SourceUpdated event with this method
        public void PrepareAnchorForDisplaying(AnchorViewModel anchorViewModel)
        {
            int x = int.Parse(anchorViewModel.XCoordinateViewModel.Location);
            int y = int.Parse(anchorViewModel.YCoordinateViewModel.Location);

            if (x <= 0.5 * anchorViewModel.Width)
            {
                if (y <= 0.5 * anchorViewModel.Height)
                {
                    anchorViewModel.AnchorDialogViewModel.DialogSite = Enums.TailSitesEnum.LeftBottomCorner;
                    return;
                }

                if (y >= (this.Height - 0.5 * anchorViewModel.Height))
                {
                    anchorViewModel.AnchorDialogViewModel.DialogSite = Enums.TailSitesEnum.LeftTopCorner;
                    return;
                }

                anchorViewModel.AnchorDialogViewModel.DialogSite = Enums.TailSitesEnum.Left;
                return;
            }

            if (x >= (this.Width - 0.5 * anchorViewModel.Width))
            {
                if (y <= 0.5 * anchorViewModel.Height)
                {
                    anchorViewModel.AnchorDialogViewModel.DialogSite = Enums.TailSitesEnum.RightBottomCorner;
                    return;
                }

                if (y >= (this.Height - 0.5 * anchorViewModel.Height))
                {
                    anchorViewModel.AnchorDialogViewModel.DialogSite = Enums.TailSitesEnum.RightTopCorner;
                    return;
                }

                anchorViewModel.AnchorDialogViewModel.DialogSite = Enums.TailSitesEnum.Right;
                return;
            }

            if (y <= 0.5 * anchorViewModel.Height)
            {
                anchorViewModel.AnchorDialogViewModel.DialogSite = Enums.TailSitesEnum.Top;
                return;
            }

            if (y >= (this.Height - 0.5 * anchorViewModel.Height))
            {
                anchorViewModel.AnchorDialogViewModel.DialogSite = Enums.TailSitesEnum.Bottom;
                return;
            }

            anchorViewModel.AnchorDialogViewModel.DialogSite = Enums.TailSitesEnum.Left;
        }
    }
}
