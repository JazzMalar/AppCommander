using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace AppCommander.Common.Converters
{
    public class NameToImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value is string)
            {
                string imageName = value as string;
                if (imageName.EndsWith("jpg",true, System.Globalization.CultureInfo.CurrentCulture) || imageName.EndsWith("jpeg", true, System.Globalization.CultureInfo.CurrentCulture))
                {
                    return new BitmapImage(new Uri(imageName)); 
                }

            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
