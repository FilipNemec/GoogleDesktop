using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Common.ViewModels;

namespace GoogleDesktop.ViewModels
{
    public class PageVM : TabVM
    {
        #region Properties
        public string Url
        {
            get => url;
            set
            {
                if (url == value) return;
                url = value;
                OnPropertyChanged(nameof(Url));
            }
        }
        #endregion

        #region Fields
        private string url;
        #endregion

        #region Constructor
        public PageVM(string name,string url):base(name)
        {
            Url = url;
        }
        #endregion

        #region Commans
        public ICommand OpenPageCommand => new RelayCommand(OpenPage);
        #endregion

        #region CommandMethods
        private void OpenPage() => System.Diagnostics.Process.Start(Url);
        #endregion
    }
}
