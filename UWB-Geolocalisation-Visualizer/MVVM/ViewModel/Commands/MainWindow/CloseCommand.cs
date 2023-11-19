using System;
using UWB_Geolocalisation_Visualizer.Core;

namespace UWB_Geolocalisation_Visualizer.MVVM.ViewModel.Commands.MainWindow
{
    public class CloseCommand : BaseCommand
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
