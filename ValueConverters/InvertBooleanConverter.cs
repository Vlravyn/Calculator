using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Calculator.WPF.ValueConverters
{
    /// <summary>
    /// Inverts the boolean value
    /// </summary>
    [ValueConversion(typeof(bool), typeof(bool))]
    public class InvertBooleanConverter : BaseValueConverter<InvertBooleanConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool val = (bool)value;
            return !val;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
