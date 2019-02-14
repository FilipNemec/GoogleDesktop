using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Common.Extensions;

namespace GoogleDesktop.Converters
{
    class LabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string s = value as string;
            if (string.IsNullOrWhiteSpace(s)) return value;

            int type = System.Convert.ToInt32(parameter);

            switch (type)
            {
                case 1: return s.Truncate(50); 
                case 2: return s.Truncate(60);
                case 3: return ConvertDescription(s);
                default: return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        private object ConvertDescription(string s)
        {
            var parts = s.SplitInParts(60);
            return string.Join(Environment.NewLine, parts.ToArray());
        }

 

    }
}
