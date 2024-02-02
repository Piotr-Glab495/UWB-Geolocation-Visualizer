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
        private int width = 800;

        [ObservableProperty]
        private int height = 725;

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
            int count = didLocalisePoint ? Anchors.Count - 1 : Anchors.Count;
            PointD[] points = new PointD[count];
            var anchorsWithoutLocalised = Anchors.Where(a => a.Id != int.MaxValue).ToArray();
            for (int i = 0; i < count; ++i)
            {
                points[i] = new PointD(
                    x: double.Parse(anchorsWithoutLocalised[i].XCoordinateViewModel.Location, NumberStyles.Float, CultureInfo.InvariantCulture),
                    y: double.Parse(anchorsWithoutLocalised[i].YCoordinateViewModel.Location, NumberStyles.Float, CultureInfo.InvariantCulture)
                );
            }
            return points;
        }

        public void UpsertLocalisedAnchor(PointD point)
        {
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
                    anchor.XCoordinateViewModel.DisplayName = anchorViewModel.XCoordinateViewModel.DisplayName;
                    anchor.XCoordinateViewModel.Location = anchorViewModel.XCoordinateViewModel.Location;
                    anchor.XCoordinateViewModel.IsEditable = anchorViewModel.XCoordinateViewModel.IsEditable;
                    anchor.YCoordinateViewModel.DisplayName = anchorViewModel.YCoordinateViewModel.DisplayName;
                    anchor.YCoordinateViewModel.Location = anchorViewModel.YCoordinateViewModel.Location;
                    anchor.YCoordinateViewModel.IsEditable = anchorViewModel.YCoordinateViewModel.IsEditable;
                    anchor.UpsertAnchorCommandViewModel = anchorViewModel.UpsertAnchorCommandViewModel;
                    anchor.AnchorDialogViewModel = anchorViewModel.AnchorDialogViewModel;
                    anchor.DisplayName = anchorViewModel.DisplayName;
                    anchor.LocationVisibility = anchorViewModel.LocationVisibility;
                    anchor.Visibility = anchorViewModel.Visibility;
                    break;
                }
            }
        }

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
                anchorViewModel.XCoordinateViewModel.Location = x.ToString("F2", CultureInfo.InvariantCulture);
                x -= 1.0;
                anchorViewModel.XCoordinateViewModel.Location = x.ToString("F2", CultureInfo.InvariantCulture);
                y += 1.0;
                anchorViewModel.YCoordinateViewModel.Location = y.ToString("F2", CultureInfo.InvariantCulture);
                y -= 1.0;
                anchorViewModel.YCoordinateViewModel.Location = y.ToString("F2", CultureInfo.InvariantCulture);
            } 
            else
            {
                //this is to ensure a correct double value is assigned to the property currently
                anchorViewModel.XCoordinateViewModel.Location = x.ToString("F2", CultureInfo.InvariantCulture);
                anchorViewModel.YCoordinateViewModel.Location = y.ToString("F2", CultureInfo.InvariantCulture);
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
            double.TryParse(BordersSetterViewModel.XBorderMinViewModel.Location, NumberStyles.Float, CultureInfo.InvariantCulture, out double borderXMin);
            double.TryParse(BordersSetterViewModel.YBorderMinViewModel.Location, NumberStyles.Float, CultureInfo.InvariantCulture, out double borderYMin);
            double.TryParse(BordersSetterViewModel.XBorderMaxViewModel.Location, NumberStyles.Float, CultureInfo.InvariantCulture, out double borderXMax);
            double.TryParse(BordersSetterViewModel.YBorderMaxViewModel.Location, NumberStyles.Float, CultureInfo.InvariantCulture, out double borderYMax);
            x -= borderXMin;
            y -= borderYMin;

            double xMaxCompareValue = 0.5 * anchorViewModel.Width;
            double yMaxCompareValue = 0.5 * anchorViewModel.Height;
            //calculating dialog site
            if (borderXMax <= xMaxCompareValue)
            {
                xMaxCompareValue =  0.5 * borderXMax;
            }
            if (borderYMax <= yMaxCompareValue)
            {
                yMaxCompareValue = 0.5 * borderYMax;
            }
            if (x <= xMaxCompareValue)
            {
                if (y <= yMaxCompareValue)
                {
                    anchorViewModel.AnchorDialogViewModel.DialogSite = Enums.TailSitesEnum.LeftBottomCorner;
                    return;
                }

                if (y >= (borderYMax - yMaxCompareValue))
                {
                    anchorViewModel.AnchorDialogViewModel.DialogSite = Enums.TailSitesEnum.LeftTopCorner;
                    return;
                }

                anchorViewModel.AnchorDialogViewModel.DialogSite = Enums.TailSitesEnum.Left;
                return;
            }

            if (x >= (borderXMax - xMaxCompareValue))
            {
                if (y <= yMaxCompareValue)
                {
                    anchorViewModel.AnchorDialogViewModel.DialogSite = Enums.TailSitesEnum.RightBottomCorner;
                    return;
                }

                if (y >= (borderYMax - yMaxCompareValue))
                {
                    anchorViewModel.AnchorDialogViewModel.DialogSite = Enums.TailSitesEnum.RightTopCorner;
                    return;
                }

                anchorViewModel.AnchorDialogViewModel.DialogSite = Enums.TailSitesEnum.Right;
                return;
            }

            if (y <= yMaxCompareValue)
            {
                anchorViewModel.AnchorDialogViewModel.DialogSite = Enums.TailSitesEnum.Top;
                return;
            }

            if (y >= (borderYMax - yMaxCompareValue))
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
