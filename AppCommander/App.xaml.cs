using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AppCommander
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Loads the Splash Screen and shows it for an amount of seconds
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            SplashScreen splash = new SplashScreen("/Common/Assets/splash.png");
            splash.Show(false);
            System.Threading.Thread.Sleep(3 * 1000);
            splash.Close(TimeSpan.Zero);
        }
    }
}
