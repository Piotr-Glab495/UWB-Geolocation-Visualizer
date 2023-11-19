using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using UWB_Geolocalisation_Visualizer.Core;
using UWB_Geolocalisation_Visualizer.MVVM.ViewModel.Enums;

namespace UWB_Geolocalisation_Visualizer.MVVM.ViewModel
{
    public partial class AnchorDialogTailViewModel : ViewModelBase
    {
        private readonly IDictionary<string, string> pointsDictionary = new Dictionary<string, string>
        {
            { "Left", "-20, 75 15, 60 15, 90"},
            { "Top", "109.5, 10 139.5, 10 124.5, -20"},
            { "Right", "270, 75 235, 60 235, 90"},
            { "Bottom", "109.5, 140 139.5, 140 124.5, 170"},
        }; 

        [ObservableProperty]
        private int left = (int)AnchorPositionLeftEnum.Left;
        
        [ObservableProperty]
        private int bottom = (int)AnchorPositionBottomEnum.LeftRight;

        [ObservableProperty]
        private string points;

        [ObservableProperty]
        private TailSitesEnum dialogSite;

        public AnchorDialogTailViewModel(string displayName, TailSitesEnum dialogSite) : base(displayName)
        {
            this.dialogSite = dialogSite;
            SwitchSideIfNeeded();
            Points = pointsDictionary["Left"]; //to suppress the warning of uniinitialized property
        }

        partial void OnDialogSiteChanged(TailSitesEnum value)
        {
            SwitchSideIfNeeded();
        }

        private void SwitchSideIfNeeded()
        {
            switch (DialogSite)
            {
                case TailSitesEnum.Right:
                    Left = (int)AnchorPositionLeftEnum.Right;
                    Bottom = (int)AnchorPositionBottomEnum.LeftRight;
                    Points = pointsDictionary["Right"];
                    break;
                case TailSitesEnum.Top:
                    Left = (int)AnchorPositionLeftEnum.TopBottom;
                    Bottom = (int)AnchorPositionBottomEnum.Top;
                    Points = pointsDictionary["Top"];
                    break;
                case TailSitesEnum.Bottom:
                    Left = (int)AnchorPositionLeftEnum.TopBottom;
                    Bottom = (int)AnchorPositionBottomEnum.Bottom;
                    Points = pointsDictionary["Bottom"];
                    break;
                default:
                    Left = (int)AnchorPositionLeftEnum.Left;
                    Bottom = (int)AnchorPositionBottomEnum.LeftRight;
                    Points = pointsDictionary["Left"];
                    break;
            }
        }
    }
}
