using Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GoogleDesktop.ViewModels
{
   public class ResultVM
    {
        #region Properties
        public string Header { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        #endregion

        #region Commands
        public ICommand OpenPageCommand => new RelayCommand(OpenPage);
        public ICommand OpenBrowserCommand => new RelayCommand(OpenBrowser);
        #endregion

        #region CommandMethods
        private void OpenPage() => MainVM.Instance.AddTab(new PageVM(Title, Link));
        private void OpenBrowser()=> System.Diagnostics.Process.Start(Link);
        #endregion
    }
}
