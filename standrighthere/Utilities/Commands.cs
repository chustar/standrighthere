using Microsoft.Phone.Net.NetworkInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace standrighthere
{
    public class SimpleCommand : ICommand
    {
        public SimpleCommand(Action<object> handler)
        {
            _handler = handler;
            _isEnabled = true;
        }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                if (value != _isEnabled)
                {
                    _isEnabled = value;
                    if (CanExecuteChanged != null)
                    {
                        CanExecuteChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        public bool CanExecute(object parameter)
        {
            return IsEnabled;
        }

        public event EventHandler CanExecuteChanged;

        public virtual void Execute(object parameter)
        {
            _handler(parameter);
        }

        private Action<object> _handler;
    }

    public class CustomCommand : ICommand
    {
        public CustomCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public virtual void Execute(object parameter)
        {
            _execute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        public event EventHandler CanExecuteChanged;

        private Action<object> _execute;
        private Func<object, bool> _canExecute;
    }

    public class NetworkEnabledCommand : SimpleCommand
    {
        public NetworkEnabledCommand(Action<object> handler)
            : base(handler)
        {
        }

        public override void Execute(object parameter)
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                base.Execute(parameter);
            }
            else
            {
                MessageBox.Show("Cannot perform this action while offline.");
            }
        }
    }
}
