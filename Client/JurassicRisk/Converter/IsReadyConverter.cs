using System;
using System.Globalization;
using System.Windows.Data;

namespace JurassicRisk.Converter
{
    public class IsReadyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string _isReady;
            if ((bool)value)
                _isReady = "✅";
            else
                _isReady = "❌";
            return _isReady;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool _isReady = System.Convert.ToBoolean(value);
            if (_isReady == true)
                return "✅";
            else
                return "❌";
        }
    }
}
