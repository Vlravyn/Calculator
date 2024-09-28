using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace Calculator.WPF.ValueConverters
{
    /// <summary>
    /// Base Converter class that all the converter classes can inherit from.
    /// </summary>
    /// <typeparam name="T">The converter class that is inheriting this class.</typeparam>
    public abstract class BaseValueConverter<T> : MarkupExtension, IValueConverter
        where T : class, new()
    {
        public T mConverter;
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return mConverter ??= new T();
        }
    }
}
