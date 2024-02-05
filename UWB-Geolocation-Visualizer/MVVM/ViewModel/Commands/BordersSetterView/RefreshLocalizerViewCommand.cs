using System;
using UWB_Geolocation_Visualizer.Core;

namespace UWB_Geolocation_Visualizer.MVVM.ViewModel.Commands.BordersSetterView
{
    class RefreshLocalizerViewCommand : CommandBase
    {
        private readonly BordersSetterViewModel model;

        public event Action RequestRefreshLocalizerView;

        public override void Execute(object? parameter)
        {
            if (!double.TryParse(model.XBorderMinViewModel.Location, out double borderXMin))
            {
                borderXMin = 0.0;
            }
            if (!double.TryParse(model.XBorderMaxViewModel.Location, out double borderXMax))
            {
                borderXMax = borderXMin;
            }

            if(borderXMin > borderXMax)
            {
                borderXMax = borderXMin;
            }

            if (!double.TryParse(model.YBorderMinViewModel.Location, out double borderYMin))
            {
                borderYMin = 0.0;
            }
            if (!double.TryParse(model.YBorderMaxViewModel.Location, out double borderYMax))
            {
                borderYMax = borderYMin;
            }

            if (borderYMin > borderYMax)
            {
                borderYMax = borderYMin;
            }

            RequestRefreshLocalizerView?.Invoke();
        }

        public RefreshLocalizerViewCommand(BordersSetterViewModel model, Action action)
        {
            RequestRefreshLocalizerView += action;
            this.model = model;
        }
    }
}
