using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Common.ViewModels;

namespace GoogleDesktop.ViewModels
{
  public  class TabVM : VMBase
    {
        #region Properties
        public string Name { get; set; }
        public bool CanClose { get; set; }
        #endregion

        #region Commands
        public ICommand CloseCommand => new RelayCommand(Close,(o)=>CanClose);

        #endregion

        #region Constructors
        public TabVM(string name,bool canClose=true)
        {
            Name = name;
            CanClose = canClose;
        }
        #endregion

        #region CommandMethods
        private void Close() => MainVM.Instance.RemoveTab(this);
        #endregion
    }
}
