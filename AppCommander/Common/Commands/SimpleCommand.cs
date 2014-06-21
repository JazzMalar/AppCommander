using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppCommander.Common.Commands
{
    public class SimpleCommand : ICommand
    {

        #region Fields

        readonly Action _execute = null;
        readonly bool _canExecute = true;

        #endregion // Fields

        #region Constructors

        public SimpleCommand(Action execute) : this(execute, true) { }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public SimpleCommand(Action execute, bool canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            
            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion // Constructors

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _execute();
        }

        #endregion // ICommand Members
    }
}
