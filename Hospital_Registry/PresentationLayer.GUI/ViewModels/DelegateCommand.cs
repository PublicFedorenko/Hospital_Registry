using PresentationLayer.GUI.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.GUI.ViewModels
{
    public class DelegateCommand : IDelegateCommand
    {
        private Action<object> _execute;
        private Func<object, bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<object> execute)
        {
            _execute = execute;
            _canExecute = AlwaysCanExecute;
        }

        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        private bool AlwaysCanExecute(object param)
        {
            return true;
        }

        public void Execute(object param)
        {
            _execute(param);
        }

        public bool CanExecute(object param)
        {
            return _canExecute(param);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
