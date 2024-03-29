﻿using CommunityToolkit.Mvvm.ComponentModel;
using System;
using UWB_Geolocation_Visualizer.MVVM.ViewModel.Commands;
using UWB_Geolocation_Visualizer.MVVM.ViewModel.Commands.BordersSetterView;

namespace UWB_Geolocation_Visualizer.MVVM.ViewModel
{
    public partial class BordersSetterViewModel : DialogViewModel
    {
        [ObservableProperty]
        private CoordinateViewModel xBorderMinViewModel;

        [ObservableProperty]
        private CoordinateViewModel xBorderMaxViewModel;

        [ObservableProperty]
        private CoordinateViewModel yBorderMinViewModel;

        [ObservableProperty]
        private CoordinateViewModel yBorderMaxViewModel;

        [ObservableProperty]
        private CommandViewModel refreshCommandViewModel;

        public BordersSetterViewModel(string displayName, Action action) : base(displayName, id: 0)
        {
            xBorderMinViewModel = new CoordinateViewModel(displayName: "xMin:", "-5000");
            xBorderMaxViewModel = new CoordinateViewModel(displayName: "xMax:", "5000");
            yBorderMinViewModel = new CoordinateViewModel(displayName: "yMin:", "-5000");
            yBorderMaxViewModel = new CoordinateViewModel(displayName: "yMax:", "5000");
            RefreshCommandViewModel = new CommandViewModel(
                displayName: "Odśwież obszar lokalizacji",
                command: new RefreshLocalizerViewCommand(this, action)
            );
        }
    }
}
