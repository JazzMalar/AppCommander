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

        public static int MaxSearchResults_Product 
        { 
            get 
            {
                return int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxSearchResults_Product"]);
            }
            set 
            {
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["MaxSearchResults_Product"].Value = value.ToString();
                config.Save(ConfigurationSaveMode.Modified);

                ConfigurationManager.RefreshSection("appSettings");
            }
        }
        public static int MaxSearchResults_Customer
        {
            get
            {
                return int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxSearchResults_Customer"]);
            }
            set
            {
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["MaxSearchResults_Customer"].Value = value.ToString();
                config.Save(ConfigurationSaveMode.Modified);

                ConfigurationManager.RefreshSection("appSettings");
            }
        }
        public static int MaxSearchResults_ProductGroup
        {
            get
            {
                return int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxSearchResults_ProductGroup"]);
            }
            set
            {
                System.Configuration.ConfigurationManager.AppSettings["MaxSearchResults_ProductGroup"] = value.ToString();
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["MaxSearchResults_ProductGroup"].Value = value.ToString();
                config.Save(ConfigurationSaveMode.Modified);

                ConfigurationManager.RefreshSection("appSettings");
            }
        }


        public static int StartSearchAfterNumKeys_Product
        {
            get
            {
                return int.Parse(System.Configuration.ConfigurationManager.AppSettings["StartSearchAfterNumKeys_Product"]);
            }
            set 
            {
                System.Configuration.ConfigurationManager.AppSettings["StartSearchAfterNumKeys_Product"] = value.ToString();
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["StartSearchAfterNumKeys_Product"].Value = value.ToString();
                config.Save(ConfigurationSaveMode.Modified);

                ConfigurationManager.RefreshSection("appSettings");
            }
        }


        public static int StartSearchAfterNumKeys_Customer
        {
            get
            {
                return int.Parse(System.Configuration.ConfigurationManager.AppSettings["StartSearchAfterNumKeys_Customer"]);
            }
            set
            {
                System.Configuration.ConfigurationManager.AppSettings["StartSearchAfterNumKeys_Customer"] = value.ToString();
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["StartSearchAfterNumKeys_Customer"].Value = value.ToString();
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
                System.Configuration.ConfigurationManager.AppSettings["LogDirectory"] = value;
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
                System.Configuration.ConfigurationManager.AppSettings["LogLevel"] = value.ToString();
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["LogLevel"].Value = value.ToString();
                config.Save(ConfigurationSaveMode.Modified);

                ConfigurationManager.RefreshSection("appSettings");
            }
        }


        public static int ShowSplashForSeconds
        {
            get
            {
                return int.Parse(System.Configuration.ConfigurationManager.AppSettings["ShowSplashForSeconds"]);
            }
            set
            {
                System.Configuration.ConfigurationManager.AppSettings["ShowSplashForSeconds"] = value.ToString();
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["ShowSplashForSeconds"].Value = value.ToString();
                config.Save(ConfigurationSaveMode.Modified);

                ConfigurationManager.RefreshSection("appSettings");
            }
        }


        public static string
 DBSource
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["DBSource"];
            }
            set
            {
                System.Configuration.ConfigurationManager.AppSettings["DBSource"] = value.ToString();
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["DBSource"].Value = value.ToString();
                config.Save(ConfigurationSaveMode.Modified);

                ConfigurationManager.RefreshSection("appSettings");
            }
        }


        public static string DBPort
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["DBPort"];
            }
            set
            {
                System.Configuration.ConfigurationManager.AppSettings["DBPort"] = value.ToString();
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["DBPort"].Value = value.ToString();
                config.Save(ConfigurationSaveMode.Modified);

                ConfigurationManager.RefreshSection("appSettings");
            }
        }


        public static string DBUser
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["DBUser"];
            }
            set
            {
                System.Configuration.ConfigurationManager.AppSettings["DBUser"] = value.ToString();
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["DBUser"].Value = value.ToString();
                config.Save(ConfigurationSaveMode.Modified);

                ConfigurationManager.RefreshSection("appSettings");
            }
        }


        public static string DBPass
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["DBPass"];
            }
            set
            {
                System.Configuration.ConfigurationManager.AppSettings["DBPass"] = value.ToString();
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["DBPass"].Value = value.ToString();
                config.Save(ConfigurationSaveMode.Modified);

                ConfigurationManager.RefreshSection("appSettings");
            }
        }


        public static string DBCatalog
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["DBCatalog"];
            }
            set
            {
                System.Configuration.ConfigurationManager.AppSettings["DBCatalog"] = value.ToString();
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["DBCatalog"].Value = value.ToString();
                config.Save(ConfigurationSaveMode.Modified);

                ConfigurationManager.RefreshSection("appSettings");
            }
        }
        
    }
}
