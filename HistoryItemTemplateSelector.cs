using Calculator.Core.OldCalculations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Calculator.WPF
{
    public class HistoryItemTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (container is not FrameworkElement element)
                return null;

            return item.GetType().Name switch
            {
                nameof(OldBase10TwoNumberCalculation) => element.FindResource(nameof(OldBase10TwoNumberCalculation)) as DataTemplate,
                nameof(OldBase10SingleNumberCalculation) => element.FindResource(nameof(OldBase10SingleNumberCalculation)) as DataTemplate
            };
        }
    }
}
