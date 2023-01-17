using JurassicRisk.ViewsModels;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Color = System.Windows.Media.Color;

namespace JurassicRisk.Converter
{
    public class OwnerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var color = Color.FromRgb(0,0,0);
            if (value.ToString() == JurassicRiskViewModel.Get.LobbyVm.Lobby.Owner)
            {
                color = Color.FromRgb(0, 150, 150);
            }


            return new SolidColorBrush(color);
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
