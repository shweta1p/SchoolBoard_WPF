using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kirin_2.ViewModel
{
    public class CommandVM : ICommand
    {
        Action<object> _executeMethod;
        Func<object, bool> canexecuteMethod;

        public CommandVM(Action<object> _executeMethod, Func<object, bool> canexecuteMethod)
        {
            this._executeMethod = _executeMethod;
            this.canexecuteMethod = canexecuteMethod;
        }

        public void Execute(object parameter)
        {
            _executeMethod(parameter);
        }

        public bool CanExecute(object o)
        {
            return true;
        }

       public event EventHandler CanExecuteChanged;
        
    }
}
