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

        private TailSitesEnum dialogSite;

        public AnchorDialogTailViewModel(string displayName, TailSitesEnum dialogSite) : base(displayName)
        {
            this.dialogSite = dialogSite;
            switch(this.dialogSite)
            {   
                case TailSitesEnum.Right:
                    left = (int)AnchorPositionLeftEnum.Right;
                    bottom = (int)AnchorPositionBottomEnum.LeftRight;
                    points = this.pointsDictionary["Right"];
                    break;
                case TailSitesEnum.Top:
                    left = (int)AnchorPositionLeftEnum.TopBottom;
                    bottom = (int)AnchorPositionBottomEnum.Top;
                    points = this.pointsDictionary["Top"];
                    break;
                case TailSitesEnum.Bottom:
                    left = (int)AnchorPositionLeftEnum.TopBottom;
                    bottom = (int)AnchorPositionBottomEnum.Bottom;
                    points = this.pointsDictionary["Bottom"];
                    break;
                default:
                    left = (int)AnchorPositionLeftEnum.Left;
                    bottom = (int)AnchorPositionBottomEnum.LeftRight;
                    points = this.pointsDictionary["Left"];
                    break;
            }
        }
    }
}
