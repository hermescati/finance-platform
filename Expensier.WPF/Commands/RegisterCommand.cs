using Expensier.Domain.Services.Authentication;
using Expensier.WPF.State.Authenticators;
using Expensier.WPF.State.Navigators;
using Expensier.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.Commands
{
    public class RegisterCommand : AsyncCommandBase
    {
        private readonly RegisterViewModel _registerViewModel;
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _registerRenavigator;

        public RegisterCommand(RegisterViewModel registerViewModel, IAuthenticator authenticator, IRenavigator renavigator)
        {
            _registerViewModel = registerViewModel;
            _authenticator = authenticator;
            _registerRenavigator = renavigator;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _registerViewModel.ErrorMessage = string.Empty; 

            try
            {
                RegistrationResult result = await _authenticator.userRegister(
                        _registerViewModel.FirstName,
                        _registerViewModel.LastName,
                        _registerViewModel.Email,
                        _registerViewModel.Password,
                        _registerViewModel.ConfirmPassword);

                switch (result)
                {
                    case RegistrationResult.Success:
                        _registerRenavigator.Renavigate();
                        break;
                    case RegistrationResult.PasswordsDoNotMatch:
                        _registerViewModel.ErrorMessage = "Password does not match the confirm password!";
                        break;
                    case RegistrationResult.PasswordTooShort:
                        _registerViewModel.ErrorMessage = "Entered password is too short. Password should be at least 8 characters!";
                        break;
                    case RegistrationResult.EmailInUse:
                        _registerViewModel.ErrorMessage = "Account already in use. Log in instead!";
                        break;
                    default:
                        _registerViewModel.ErrorMessage = "Registration failed!";
                        break;
                }
            }
            catch (Exception)
            {
                _registerViewModel.ErrorMessage = "Registration failed!";
            }
        }
    }
}
