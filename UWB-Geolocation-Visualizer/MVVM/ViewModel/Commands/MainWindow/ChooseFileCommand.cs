using Microsoft.Win32;
using System;
using System.Windows.Media.Imaging;
using UWB_Geolocation_Visualizer.Core;

namespace UWB_Geolocation_Visualizer.MVVM.ViewModel.Commands.MainWindow
{
    public class ChooseFileCommand : BaseCommand
    {
        private LocalizerViewModel localizerViewModel;

        public ChooseFileCommand(LocalizerViewModel localizerViewModel)
        {
            this.localizerViewModel = localizerViewModel;
        }

        public override void Execute(object? parameter)
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "Image Files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp",
                Title = "Wybierz tło obszaru lokalizacji"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedImagePath = openFileDialog.FileName;
                BitmapImage bitmapImage = new(new Uri(selectedImagePath));
                localizerViewModel.LocalizerBgSource = bitmapImage;
            }
        }
    }
}
