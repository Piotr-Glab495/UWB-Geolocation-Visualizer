using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using UWB_Geolocation_Library.SimpleTypes;
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
        private BordersSetterViewModel bordersSetterViewModel;

        private bool didLocalisePoint = false;

        public LocalizerViewModel(string displayName) : base(displayName) 
        {
            BordersSetterViewModel = new BordersSetterViewModel("Podaj granice obszaru lokalizacji", OnSetBorders);
        }

        public static void LocatingError(string msg)
        {
            string messageBoxText = "Wystąpił błąd biblioteki: " + msg + "!";
            string caption = "Błąd";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Error;

            _ = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
        }

        public PointD[] AnchorsToPointDArray()
        {
            int count = didLocalisePoint ? Anchors.Count : Anchors.Count - 1;
            PointD[] points = new PointD[count];
            var anchorsWithoutLocalised = Anchors.Where(a => a.Id != int.MaxValue).ToArray();
            for (int i = 0; i < count; ++i)
            {
                points[i] = new PointD(
                    x: double.Parse(anchorsWithoutLocalised[i].XCoordinateViewModel.Location),
                    y: double.Parse(anchorsWithoutLocalised[i].YCoordinateViewModel.Location)
                );
            }
            return points;
        }

        public void UpsertLocalisedAnchor(PointD point)
        {
            //TODO: check if the localisedPointViewModel is considered the same
            AnchorViewModel localisedPointViewModel = new(point);
            UpsertAnchor(localisedPointViewModel);
            didLocalisePoint = true;
        }

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
        /**
         * <summary>
         * Method parsing the coordinates locations to double precision numbers, optionally updating their values if they made no sense and calculating 
         * the site of AnchorView's Ellipse on which the AnchorView's dialog should be then displayed.
         * </summary>
         */
        private void PrepareAnchorForDisplaying(AnchorViewModel anchorViewModel, bool doFakeCoordinateChange = false)
        {
            double x = ReadProperCoordinateValue(
                anchorViewModel.XCoordinateViewModel.Location,
                BordersSetterViewModel.XBorderMinViewModel.Location,
                BordersSetterViewModel.XBorderMaxViewModel.Location
            );
            double y = ReadProperCoordinateValue(
                anchorViewModel.YCoordinateViewModel.Location,
                BordersSetterViewModel.YBorderMinViewModel.Location,
                BordersSetterViewModel.YBorderMaxViewModel.Location
            );

            //this fake update is used to make anchorView change due to external borders changes
            if (doFakeCoordinateChange)
            {
                x += 1.0;
                anchorViewModel.XCoordinateViewModel.Location = x.ToString();
                x -= 1.0;
                anchorViewModel.XCoordinateViewModel.Location = x.ToString();
                y += 1.0;
                anchorViewModel.YCoordinateViewModel.Location = y.ToString();
                y -= 1.0;
                anchorViewModel.YCoordinateViewModel.Location = y.ToString();
            } 
            else
            {
                //this is to ensure a correct double value is assigned to the property currently
                anchorViewModel.XCoordinateViewModel.Location = x.ToString();
                anchorViewModel.YCoordinateViewModel.Location = y.ToString();
            }

            this.AdjustDialogSite(anchorViewModel, x, y);
        }

        /**
         * <summary>
         * Method calculating the site of AnchorView's Ellipse on which the AnchorView's dialog should be then displayed.
         * </summary>
         */
        private void AdjustDialogSite(AnchorViewModel anchorViewModel, double x, double y)
        {
            //getting back to world (px) not map (OXY) coordinates
            double borderXMin = double.Parse(BordersSetterViewModel.XBorderMinViewModel.Location);
            double borderYMin = double.Parse(BordersSetterViewModel.YBorderMinViewModel.Location);
            double borderXMax = double.Parse(BordersSetterViewModel.XBorderMaxViewModel.Location);
            double borderYMax = double.Parse(BordersSetterViewModel.YBorderMaxViewModel.Location);
            x -= borderXMin;
            y -= borderYMin;

            //calculating dialog site
            if (x <= 0.5 * anchorViewModel.Width)
            {
                if (y <= 0.5 * anchorViewModel.Height)
                {
                    anchorViewModel.AnchorDialogViewModel.DialogSite = Enums.TailSitesEnum.LeftBottomCorner;
                    return;
                }

                if (y >= (borderYMax - 0.5 * anchorViewModel.Height))
                {
                    anchorViewModel.AnchorDialogViewModel.DialogSite = Enums.TailSitesEnum.LeftTopCorner;
                    return;
                }

                anchorViewModel.AnchorDialogViewModel.DialogSite = Enums.TailSitesEnum.Left;
                return;
            }

            if (x >= (borderXMax - 0.5 * anchorViewModel.Width))
            {
                if (y <= 0.5 * anchorViewModel.Height)
                {
                    anchorViewModel.AnchorDialogViewModel.DialogSite = Enums.TailSitesEnum.RightBottomCorner;
                    return;
                }

                if (y >= (borderYMax - 0.5 * anchorViewModel.Height))
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

            if (y >= (borderYMax - 0.5 * anchorViewModel.Height))
            {
                anchorViewModel.AnchorDialogViewModel.DialogSite = Enums.TailSitesEnum.Bottom;
                return;
            }

            anchorViewModel.AnchorDialogViewModel.DialogSite = Enums.TailSitesEnum.Left;
        }

        /**
         * <summary>
         *  This is a method used to parse the location string to a double, bewaring of possible problems: invalid format, value lower than lower border or higher than higher border.
         * </summary> 
         */
        private static double ReadProperCoordinateValue(string coordinateToRead, string borderMinToRead, string borderMaxToRead)
        {
            if (!double.TryParse(coordinateToRead, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
            {
                if (!double.TryParse(borderMinToRead, out value))
                {
                    value = 0.0;
                    return value;
                }
                return value;
            }

            if (double.TryParse(borderMinToRead, out double borderMin) && value < borderMin)
            {
                return borderMin;
            }

            if (double.TryParse(borderMaxToRead, out double borderMax) && value > borderMax)
            {
                return borderMax;
            }

            return value;
        }

        /**
         * <summary>
         *  A method doing a fake update of the anchors coordinates to make their view change due to borders changes
         * </summary>
         */
        private void OnSetBorders()
        {
            foreach(AnchorViewModel anchor in Anchors)
            {
                PrepareAnchorForDisplaying(anchor, doFakeCoordinateChange: true);
            }
        }
    }
}
