using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using UWB_Geolocation_Visualizer.Core;
using UWB_Geolocation_Visualizer.MVVM.ViewModel.Enums;

namespace UWB_Geolocation_Visualizer.MVVM.ViewModel
{
    public partial class AnchorDialogTailViewModel : ViewModelBase
    {
        private readonly IDictionary<string, string> pointsDictionary = new Dictionary<string, string>
        {
            { "Left", "7, 7.5 40, -10 40, 25"},
            { "Top", "7, 7.5 -10, -26.5 25, -26.5"},
            { "Right", "7, 7.5 -26, -10 -26, 25"},
            { "Bottom", "7, 7.5 -10, 41.5 25, 41.5"},
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
