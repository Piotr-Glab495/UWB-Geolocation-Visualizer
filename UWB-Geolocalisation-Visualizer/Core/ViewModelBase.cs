using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System;

namespace UWB_Geolocalisation_Visualizer.Core
{
    public partial class ViewModelBase : ObservableObject
    {
        [ObservableProperty]
        private string displayName = "";

        public ViewModelBase(string displayName)
        {
            this.displayName = displayName;
        }
    }
}
