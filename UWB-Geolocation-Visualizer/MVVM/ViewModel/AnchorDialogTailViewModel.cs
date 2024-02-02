using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using UWB_Geolocation_Visualizer.Core;
using UWB_Geolocation_Visualizer.MVVM.ViewModel.Enums;

namespace UWB_Geolocation_Visualizer.MVVM.ViewModel
{
    public partial class AnchorDialogViewModel : ViewModelBase
    {
        private readonly IDictionary<string, string> pointsDictionary = new Dictionary<string, string>
        {
            { "Left", "0.5, 0 32.5, -17.5 32.5, 17.5"},
            { "Top", "0, 0 -17, -35 17, -35"},
            { "Right", "0, 0 -26, -17.5 -26, 17.5"},
            { "Bottom", "0, 0 -17, 19 18, 19"},
            { "LeftTop", "-0.5, 0 0, 26.5 35, 26.5"},
            { "RightTop", "0, 0 -27.5, 19 0, 25"},
            { "LeftBottom", "0, 0 0, -46 35, -35"},
            { "RightBottom", "0, 0 -27.5, -35 0, -41.5"},
        }; 

        [ObservableProperty]
        private int left = (int)AnchorPositionLeftEnum.Left;
        
        [ObservableProperty]
        private int bottom = (int)AnchorPositionBottomEnum.LeftRight;

        [ObservableProperty]
        private string points;

        [ObservableProperty]
        private TailSitesEnum dialogSite;

        public AnchorDialogViewModel(string displayName, TailSitesEnum dialogSite) : base(displayName)
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
                case TailSitesEnum.LeftTopCorner:
                    Left = (int)AnchorPositionLeftEnum.LeftCorner;
                    Bottom = (int)AnchorPositionBottomEnum.Bottom;
                    Points = pointsDictionary["LeftTop"];
                    break;
                case TailSitesEnum.RightTopCorner:
                    Left = (int)AnchorPositionLeftEnum.RightCorner;
                    Bottom = (int)AnchorPositionBottomEnum.Bottom;
                    Points = pointsDictionary["RightTop"];
                    break;
                case TailSitesEnum.LeftBottomCorner:
                    Left = (int)AnchorPositionLeftEnum.LeftCorner;
                    Bottom = (int)AnchorPositionBottomEnum.Top;
                    Points = pointsDictionary["LeftBottom"];
                    break;
                case TailSitesEnum.RightBottomCorner:
                    Left = (int)AnchorPositionLeftEnum.RightCorner;
                    Bottom = (int)AnchorPositionBottomEnum.Top;
                    Points = pointsDictionary["RightBottom"];
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
