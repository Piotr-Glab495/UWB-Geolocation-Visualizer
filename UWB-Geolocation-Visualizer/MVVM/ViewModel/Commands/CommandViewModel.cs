using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Windows.Input;
using UWB_Geolocation_Visualizer.Core;

namespace UWB_Geolocation_Visualizer.MVVM.ViewModel.Commands
{
    public partial class CommandViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ICommand command;

        public CommandViewModel(string displayName, ICommand command) : base(displayName)
        {
            this.Command = command ?? throw new ArgumentNullException(nameof(command));
        }
    }
}
