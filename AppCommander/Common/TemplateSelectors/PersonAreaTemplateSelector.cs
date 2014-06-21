using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BioBS.Common.TemplateSelectors
{
    public class PersonAreaTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;

            if (
                element != null &&
                item != null &&
                item is string &&
                !string.IsNullOrWhiteSpace(item.ToString())
                )
            {
                return element.FindResource("PersonSearchTemplate") as DataTemplate;
            }
            else
                return element.FindResource("PersonOverviewTemplate") as DataTemplate;
        }
    }
}
