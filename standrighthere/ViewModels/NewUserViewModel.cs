using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace standrighthere.ViewModels
{
    class NewUserViewModel
    {
        private string _email = System.String.Empty;
        public string Email
        {
            get
            {
                return _email;
            }
            set 
            {
                _email = value;
                JoinCommand.RaiseCanExecuteChanged();
            }
        }

        private string _username = System.String.Empty;
        public string Username
        {
            get
            {
                return _username;
            }
            set 
            {
                _username = value;
                JoinCommand.RaiseCanExecuteChanged();
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        private string _password = System.String.Empty;
        public string Password
        {
            get
            {
                return _password;
            }
            set 
            {
                _password = value;
                JoinCommand.RaiseCanExecuteChanged();
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        private CustomCommand _joinCommand;
        public CustomCommand JoinCommand
        {
            get
            {
                if (_joinCommand == null)
                {
                    _joinCommand = new CustomCommand(
                        async (object parameter) =>
                        {
                            try
                            {
                                var user = new ParseUser()
                                {
                                    Email = Email,
                                    Username = Username,
                                    Password = Password
                                };

                                await user.SignUpAsync();
                                var userDetails = new ParseObject("UserDetails")
                                {
                                    {"user", user}
                                };
                                await userDetails.SaveAsync();

                                App.RootFrame.Navigate(new Uri("/UserDetailsManager.xaml", UriKind.Relative));
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("That user name has already been taken. Please try another one.");
                            }
                        },
                        (object parameter) =>
                        {
                            return Email.Trim() != System.String.Empty && Username.Trim() != System.String.Empty && Password.Trim() != System.String.Empty;
                        });
                }
                return _joinCommand;
            }
        }

        private CustomCommand _loginCommand;
        public CustomCommand LoginCommand
        {
            get
            {
                if (_loginCommand == null)
                {
                    _loginCommand = new CustomCommand(
                        async (object parameter) =>
                        {
                            try
                            {
                                await ParseUser.LogInAsync(Username, Password);

                                App.RootFrame.Navigate(new Uri("/UserDetailsManager.xaml", UriKind.Relative));
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("The password is incorrect.");
                            }
                        },
                        (object parameter) =>
                        {
                            return Username.Trim() != System.String.Empty && Password.Trim() != System.String.Empty;
                        });
                }
                return _loginCommand;
            }
        }
    }
}
