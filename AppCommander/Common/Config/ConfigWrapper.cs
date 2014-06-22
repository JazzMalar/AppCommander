using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCommander.Common.Config
{
    public static class ConfigWrapper
    {

        public static string XMLPath 
        { 
            get 
            {
                return Properties.Settings.Default.XMLPath; 
            }
            set 
            {
                Properties.Settings.Default.XMLPath = value;
                Properties.Settings.Default.Save(); 
            }
        }

        public static string LogDirectory
        {
            get
            {
                return Properties.Settings.Default.LogDirectory;
            }
            set
            {
                Properties.Settings.Default.LogDirectory = value;
                Properties.Settings.Default.Save();
            }
        }

        public static int LogLevel
        {
            get
            {
                return Properties.Settings.Default.LogLevel;
            }
            set
            {
                Properties.Settings.Default.LogLevel = value;
                Properties.Settings.Default.Save();
            }
        }
    }
}
