using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GoogleDesktop.Util
{
   public static class WebBrowserUtility
    {

            public static readonly DependencyProperty urlProperty =
                DependencyProperty.RegisterAttached("SourceUrl", typeof(string), typeof(WebBrowserUtility), new UIPropertyMetadata(null, SourceUrlPropertyChanged));


        public static string GetSourceUrl(DependencyObject obj)
            {
                return (string)obj.GetValue(urlProperty);
            }

            public static void SetSourceUrl(DependencyObject obj, string value)
            {
                obj.SetValue(urlProperty, value);
            }

            public static void SourceUrlPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                WebBrowser browser = d as WebBrowser;
                if (browser != null)
                {
                    string uri = e.NewValue as string;
                    browser.Source = !String.IsNullOrEmpty(uri) ? new Uri(uri) : null;
                }
            }       
    }
}
