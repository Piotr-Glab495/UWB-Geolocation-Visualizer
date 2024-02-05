using System;
using UWB_Geolocation_Visualizer.Core;

namespace UWB_Geolocation_Visualizer.MVVM.ViewModel.Commands.MainWindow
{
    public class CloseCommand : CommandBase
    {
        public event Action RequestClose;

        public override void Execute(object? parameter)
        {
            RequestClose?.Invoke();
        }

        public CloseCommand(Action action)
        {
            RequestClose += action;
        }
    }
}
