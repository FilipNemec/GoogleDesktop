using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleDesktop
{
   public static class Settings
    {
        #region Properties
        public static string ApiKey { get; set; }
        public static string SearchEngineId { get; set; }
        #endregion

        #region Constructor
        static Settings()
        {
            LoadConfig();
        }
        #endregion

        #region Methods
        private static void LoadConfig()
        {
            ApiKey = ConfigurationManager.AppSettings["ApiKey"];
            SearchEngineId = ConfigurationManager.AppSettings["SearchEngineId"];
        }

        public static void AddValue(string key, string value)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[key].Value = value;
            configuration.Save();
            ConfigurationManager.RefreshSection("appSettings");
            LoadConfig();
        }
        #endregion
    }
}
