using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Input;
using Common.ViewModels;

namespace GoogleDesktop.ViewModels
{
    public  class SettingsVM
    {

        #region Properties
        public string ApiKey { get; set; }
        public string SearchEngineId { get; set; }
        #endregion

        #region Constructor
        public SettingsVM()
        {
            ApiKey = Settings.ApiKey;
            SearchEngineId=Settings.SearchEngineId;
        }
        #endregion

        #region Commands
        public ICommand SaveCommand => new RelayCommand(Save);

        private void Save()
        {
            Settings.AddValue("ApiKey", ApiKey);
            Settings.AddValue("SearchEngineId", SearchEngineId);
        }
        #endregion

        #region CommandMethods

        #endregion
    }
}
