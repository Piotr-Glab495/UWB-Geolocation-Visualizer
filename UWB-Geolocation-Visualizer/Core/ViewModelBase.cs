using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace UWB_Geolocation_Visualizer.Core
{
    public abstract partial class ViewModelBase : ObservableObject, IEquatable<ViewModelBase>
    {
        [ObservableProperty]
        private string displayName = "";

        public ViewModelBase(string displayName)
        {
            this.displayName = displayName;
        }

        public override bool Equals(object? obj)
        {
            if (obj is ViewModelBase other)
            {
                return string.Equals(DisplayName, other.DisplayName);
            }
            return false;
        }

        public bool Equals(ViewModelBase? other) => Equals(other as object);

        public override int GetHashCode()
        {
            return DisplayName?.GetHashCode() ?? 0;
        }
    }
}
