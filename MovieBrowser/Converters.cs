using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Markup;

using System.Collections;
using System.Collections.Generic;

namespace MovieBrowser
{
    /// <summary>
    /// Converts any list to string
    /// </summary>
    public class IListConverter : MarkupExtension, IValueConverter
    {
        private static readonly string delimetre = ", ";

        private static IListConverter _converter = null;
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_converter == null) _converter = new IListConverter();
            return _converter;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is IList)
            {
                string outp = "";

                foreach (var p in (IList)value)
                    outp += p.ToString() + delimetre;
                outp = outp.Remove(outp.Length - delimetre.Length, delimetre.Length);

                return outp;
            }
            return "error";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    /// <summary>
    /// Converts any list to string
    /// </summary>
    public class BoolInverter : MarkupExtension, IValueConverter
    {
        private static readonly string delimetre = ", ";

        private static BoolInverter _converter = null;
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _converter ?? (_converter = new BoolInverter());
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
                return !(bool)value;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
                return !(bool)value;
            return false;
        }
    }
}
