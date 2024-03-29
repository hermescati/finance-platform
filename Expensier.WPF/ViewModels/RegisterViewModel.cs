﻿using Expensier.WPF.Commands;
using Expensier.WPF.Commands.Authentication;
using Expensier.WPF.State.Authenticators;
using Expensier.WPF.State.Navigators;
using Expensier.WPF.ViewModels.Errors;
using System.Windows.Input;


namespace Expensier.WPF.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private readonly IAuthenticator authenticator;
        private readonly IRenavigator registerRenavigator;


        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged( nameof( FirstName ) );
                OnPropertyChanged( nameof( CanRegister ) );
            }
        }


        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged( nameof( LastName ) );
                OnPropertyChanged( nameof( CanRegister ) );
            }
        }


        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged( nameof( Email ) );
                OnPropertyChanged( nameof( CanRegister ) );
            }
        }


        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged( nameof( Password ) );
                OnPropertyChanged( nameof( CanRegister ) );
            }
        }


        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged( nameof( ConfirmPassword ) );
                OnPropertyChanged( nameof( CanRegister ) );
            }
        }


        private bool _isChecked = false;
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                _isChecked = value;
                OnPropertyChanged( nameof( IsChecked ) );
                OnPropertyChanged( nameof( CanRegister ) );
            }
        }


        public bool CanRegister => !string.IsNullOrEmpty( FirstName ) &&
            !string.IsNullOrEmpty( LastName ) &&
            !string.IsNullOrEmpty( Email ) &&
            !string.IsNullOrEmpty( Password ) &&
            !string.IsNullOrEmpty( ConfirmPassword ) &&
            IsChecked == true;


        public MessageViewModel ErrorMessageViewModel { get; }
        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }


        public ICommand RegisterCommand { get; }
        public ICommand ViewLoginCommand { get; }


        public RegisterViewModel( IRenavigator loginRenavigator, IAuthenticator authenticator )
        {
            ErrorMessageViewModel = new MessageViewModel();

            RegisterCommand = new RegisterCommand( this, authenticator, loginRenavigator );
            ViewLoginCommand = new RenavigateCommand( loginRenavigator );
        }
    }
}
