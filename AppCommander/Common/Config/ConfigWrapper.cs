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
                return System.Configuration.ConfigurationManager.AppSettings["XMLPath"];
            }
            set 
            {
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["XMLPath"].Value = value;
                config.Save(ConfigurationSaveMode.Modified);

                ConfigurationManager.RefreshSection("appSettings");
            }
        }


        public static string LogDirectory
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["LogDirectory"];
            }
            set
            {
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["LogDirectory"].Value = value;
                config.Save(ConfigurationSaveMode.Modified);

                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        public static int LogLevel
        {
            get
            {
                return int.Parse(System.Configuration.ConfigurationManager.AppSettings["LogLevel"]);
            }
            set
            {
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["LogLevel"].Value = value.ToString();
                config.Save(ConfigurationSaveMode.Modified);

                ConfigurationManager.RefreshSection("appSettings");
            }
        }
    }
}
