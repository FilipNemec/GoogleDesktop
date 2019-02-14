using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Common.ViewModels
{
    public class RelayCommand : ICommand
    {
        #region Properties
        private Action<object> ExecuteAction { get; set; }
        private Func<object, bool> CanExecuteAction { get; set; }
        private Action AfterExecute { get; set; }
        #endregion

        #region EventHandlers
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        #endregion

        #region Constructors
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.ExecuteAction = execute;
            this.CanExecuteAction = canExecute;
        }
        public RelayCommand(Action execute, Func<object, bool> canExecute = null)
        {
            this.ExecuteAction = (o => execute());
            this.CanExecuteAction = canExecute;
        }
        public RelayCommand(Action execute, Action afterExecute)
        {

            this.ExecuteAction = (o => execute());
            this.AfterExecute = afterExecute;
        }
        #endregion

        #region ExecuteMethods
        public bool CanExecute(object parameter)=> this.CanExecuteAction == null || this.CanExecuteAction(parameter);

        public void Execute(object parameter)
        {
            ExecuteAction?.Invoke(parameter);
            AfterExecute?.Invoke();
        }
        public void AddAfterExecute(Action afterExecute) => this.AfterExecute = afterExecute;
        #endregion
    }
}
