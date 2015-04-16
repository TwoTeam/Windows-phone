/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace EventHub
{
    class ImageConverter 
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var PicURL = value as Photo1;
            if (PicURL != null)
            {
                var x = PicURL.prefix + "300x300" + PicURL.suffix;
                return x;
            }
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
*/