using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AppCommander.Model;
using System.IO;

namespace AppCommander.Common.TemplateSelectors
{
    public class EditImageTemplateSelector : DataTemplateSelector
    {

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;

            if (!(item is Appl)) throw new Exception("Wow... how did you get here?");

            Appl a = item as Appl; 

            if(
                a.Picture == null || 
                !(File.Exists(new Uri(a.Picture).ToString()))
              )
            {
                return element.FindResource("noImageTemplate") as DataTemplate; 
            } 
            else
            {
                return element.FindResource("imageTemplate") as DataTemplate;
            }


        }
    }
}
