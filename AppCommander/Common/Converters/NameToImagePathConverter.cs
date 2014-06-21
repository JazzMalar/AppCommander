using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AppCommander.Common.Converters
{
    public class NameToImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value is string)
            {
                string imageName = value as string;
                string imagePrefix = "pack://application:,,,/PictureLibrary;component/Resources/";

                return string.Format("{0}{1}", imagePrefix, imageName);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
