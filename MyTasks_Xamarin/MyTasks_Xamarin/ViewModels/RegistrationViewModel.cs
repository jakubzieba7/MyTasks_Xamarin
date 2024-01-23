using System;
using Xamarin.Forms;

namespace MyTasks_Xamarin.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {
        private string _userName;
        private string _email;
        private string _userRole;
        private string _password;
        private string _passwordConfirmed;

        public RegistrationViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }
        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(UserName)
                && !String.IsNullOrWhiteSpace(Email)
                && !String.IsNullOrWhiteSpace(UserRole)
                && !String.IsNullOrWhiteSpace(Password)
                && !String.IsNullOrWhiteSpace(PasswordConfirmed)
                && String.ReferenceEquals(Password,PasswordConfirmed);
        }

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string UserRole
        {
            get => _userRole;
            set => SetProperty(ref _userRole, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string PasswordConfirmed
        {
            get => _passwordConfirmed;
            set => SetProperty(ref _passwordConfirmed, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            var model = new RegistrationViewModel()
            {
                UserName = UserName,
                Email = Email,
                UserRole = UserRole,
                Password = Password,
                PasswordConfirmed = PasswordConfirmed,
            };

            var response = await RegistrationService.RegisterUserAsync(model);

            if (!response.IsSuccess)
                await ShowErrorAlert(response);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
