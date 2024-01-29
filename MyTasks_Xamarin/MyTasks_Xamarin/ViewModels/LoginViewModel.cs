using MyTasks_Xamarin.Services;
using MyTasks_Xamarin.Views;
using System;
using Xamarin.Forms;

namespace MyTasks_Xamarin.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private LoginService _loginService = new LoginService();
        private TokenService _tokenService = new TokenService();
        private string _userName;
        private string _password;

        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            this.PropertyChanged +=
                (_, __) => LoginCommand.ChangeCanExecute();
        }

        private async void OnLoginClicked(object obj)
        {
            var model = new LoginViewModel()
            {
                UserName = UserName,
                Password = Password,
            };

            var response = await _loginService.LoginAsync(model);

            if (!response.IsSuccess)
                await ShowErrorAlert(response);

            var responseToken = await _tokenService.GetAccessTokenAsync(UserName, Password);

            if (!responseToken.IsSuccessStatusCode)
                await ShowErrorAlert(response);

            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(UserName)
                && !String.IsNullOrWhiteSpace(Password);
        }

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
    }
}
