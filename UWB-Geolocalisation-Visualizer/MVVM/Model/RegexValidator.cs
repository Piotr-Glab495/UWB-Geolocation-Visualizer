using System.Text.RegularExpressions;

namespace UWB_Geolocalisation_Visualizer.MVVM.Model
{
    public class RegexValidator
    {
        private readonly Regex regex;

        public RegexValidator(string pattern)
        {
            regex = new Regex(pattern);
        }

        public string Validate(string parameter)
        {
            return regex.Replace(parameter, "");
        }
    }
}
