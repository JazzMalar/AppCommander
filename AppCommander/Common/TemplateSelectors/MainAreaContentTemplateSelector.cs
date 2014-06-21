using System;
using AppCommander.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace AppCommander.Common.TemplateSelectors
{
    public class MainAreaContentTemplateSelector : DataTemplateSelector
    {
        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;

            if (
                element == null ||
                item == null ||
                string.IsNullOrWhiteSpace(item.ToString())
                )
            {
                return element.FindResource("EmptyOverviewTemplate") as DataTemplate;
            }
            else if (item is ProductGroup)
            {
                return element.FindResource("GroupItemTemplate") as DataTemplate;
            }
            else if (item is Product)
            {
                return element.FindResource("ProductTemplate") as DataTemplate;
            }

            return null;
        }
    }
}
