using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using AppCommander.Model;

namespace AppCommander.Common.Converters
{
    public class XMLVersionToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return "0.0.0";
            return (value as XMLVersion).ToString(); 
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new XMLVersion((string)value);
        }
    }
}
