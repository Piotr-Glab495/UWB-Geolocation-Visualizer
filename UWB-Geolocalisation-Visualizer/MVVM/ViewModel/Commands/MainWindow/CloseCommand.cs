using System;
using System.Windows.Input;

namespace UWB_Geolocalisation_Visualizer.MVVM.ViewModel.Commands.MainWindow
{
    public class CloseCommand : ICommand
    {
        public event Action RequestClose = delegate { };
        public event EventHandler? CanExecuteChanged = delegate { };

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            RequestClose?.Invoke();
        }
    }
}
