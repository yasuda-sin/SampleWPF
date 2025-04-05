using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfApp1.Helpers
{
    internal class DateTimeToStringConverter : IValueConverter
    {
        public string Format { get; set; } = "yyyy/MM/dd HH:mm";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dt)
            {
                return dt == DateTime.MinValue ? "-" : dt.ToString(Format);
            }
            return "-";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str && DateTime.TryParse(str, out var result))
            {
                return result;
            }
            return DateTime.MinValue;
        }
    }
}