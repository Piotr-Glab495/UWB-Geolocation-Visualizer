using System;
using UWB_Geolocation_Visualizer.Core;

namespace UWB_Geolocation_Visualizer.MVVM.ViewModel.Commands.AnchorView
{
    public class UpsertAnchorCommand : BaseCommand
    {
        public event Action RequestUpsertAnchor = delegate { };

        private readonly AnchorViewModel anchorViewModel;

        private readonly LocalizerViewModel localizerViewModel;

        public override void Execute(object? parameter)
        {
            localizerViewModel.UpsertAnchor(anchorViewModel);
            RequestUpsertAnchor?.Invoke();

            foreach (Delegate d in RequestUpsertAnchor!.GetInvocationList())
            {
                RequestUpsertAnchor -= (Action)d;
            }
            RequestUpsertAnchor += delegate { };
        }

        public UpsertAnchorCommand(AnchorViewModel anchorViewModel, LocalizerViewModel localizerViewModel, Action action)
        {
            this.anchorViewModel = anchorViewModel;
            this.localizerViewModel = localizerViewModel;
            RequestUpsertAnchor += action;
        }

        public UpsertAnchorCommand(AnchorViewModel anchorViewModel, LocalizerViewModel localizerViewModel, Action[] requestAddAnchorDelegates)
        {
            this.anchorViewModel = anchorViewModel;
            this.localizerViewModel = localizerViewModel;
            foreach (Action action in requestAddAnchorDelegates) { 
                RequestUpsertAnchor += action;
            }
        }
    }
}
