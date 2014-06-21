using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace AppCommander.Common.Converters
{
    public class RgbToColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //TODO: this Converter was used in our Projektwoche, copied for simplicity. It may need change.


            // object is of format "rrr ggg bbb" (decimal format of rgb e.g. 127 255 0)
            //TODO: add errorhandling
            if (!(value is string))
                return value;

            string[] rgb = value.ToString().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            //TODO: add errorhandling
            if (rgb.Count() == 0 || rgb.Count() > 3)
                return value;

            //TODO: add errorhandling
            byte a = (byte)255;

            byte r = System.Convert.ToByte(rgb[0], 10);
            byte g = System.Convert.ToByte(rgb[1], 10);
            byte b = System.Convert.ToByte(rgb[2], 10);

            Color color = Color.FromArgb(a,r,g,b);
            return new SolidColorBrush(color);
        }

        // since no twoway binding on color (readonly), 
        // there is no convertback implementation needed
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
