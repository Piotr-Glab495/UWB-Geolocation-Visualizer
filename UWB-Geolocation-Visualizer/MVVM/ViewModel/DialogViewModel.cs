using CommunityToolkit.Mvvm.ComponentModel;
using UWB_Geolocation_Visualizer.Core;

namespace UWB_Geolocation_Visualizer.MVVM.ViewModel
{
    public partial class DialogViewModel : ViewModelBase
    {
        public int Id { get; set; }

        [ObservableProperty]
        private string visibility = "Collapsed";

        [ObservableProperty]
        private string borderBackground = "Transparent";

        public DialogViewModel(string displayName, int id) : base(displayName)
        {
            Id = id;
        }

        partial void OnVisibilityChanged(string value)
        {
            BorderBackground = (value == "Visible" ? "#3d8693" : "Transparent");
        }

        public bool Equals(DialogViewModel? other)
        {
            if (other is null)
                return false;

            return Id == other.Id;
        }

        public override bool Equals(object? obj) => Equals(obj as DialogViewModel);

        public override int GetHashCode() => Id.GetHashCode();
    }
}
