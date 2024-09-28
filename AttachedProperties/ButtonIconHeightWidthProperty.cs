using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Calculator.WPF.AttachedProperties
{
    public class ButtonIconHeightWidthProperty : Button
    {
        private double _iconHeightWidth;

        public double IconHeightWidth
        {
            get { return _iconHeightWidth; }
            set { _iconHeightWidth = value; }
        }


        public static readonly DependencyProperty IconHeightWidthProperty = DependencyProperty.RegisterAttached(nameof(IconHeightWidth),typeof(double), typeof(ButtonBase), new PropertyMetadata(new PropertyChangedCallback(OnHeightWidthChanged)));

        private static void OnHeightWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        public static double GetValue(DependencyObject d) => (double)d.GetValue(IconHeightWidthProperty);

        public static void SetValue(DependencyObject d, double value) => d.SetValue(IconHeightWidthProperty, value);
    }
}
