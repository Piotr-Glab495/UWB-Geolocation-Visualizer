using System;
using System.Text.RegularExpressions;
using System.Windows.Input;
using UWB_Geolocalisation_Visualizer.Core;

namespace UWB_Geolocalisation_Visualizer.MVVM.ViewModel.Commands.Anchor
{
    public class RegexValidationCommand : BaseCommand
    {
        private readonly Regex regex;

        public RegexValidationCommand(string pattern)
        {
            this.regex = new Regex(pattern);
        }
        
        public override void Execute(object? parameter)
        {
            if (parameter is TextCompositionEventArgs e)
            {
                e.Handled = !regex.IsMatch(e.Text);
            }
        }
    }
}
