using MyTasks_Xamarin.Services;
using MyTasks_Xamarin.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyTasks_Xamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private LoginViewModel _viewModel;

        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = _viewModel = new LoginViewModel();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegistrationPage());
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (_viewModel.UserName == "username" && _viewModel.Password == "password")
            {
                Application.Current.MainPage = new AppShell();
                await Shell.Current.GoToAsync("//AboutPage");
                //await Shell.Current.GoToAsync("//ItemsPage");
            }
            else
                await DisplayAlert("Ops...", "Username or Password is incorrect!", "Ok");
        }
    }
}