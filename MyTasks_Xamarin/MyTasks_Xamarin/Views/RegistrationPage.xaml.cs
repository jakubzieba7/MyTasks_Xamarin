using MyTasks_Xamarin.Services;
using MyTasks_Xamarin.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyTasks_Xamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        RegistrationService _registrationService = new RegistrationService();
        private RegistrationViewModel _viewModel;

        public RegistrationPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new RegistrationViewModel();
        }

        private async void ButtonCancel_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new AppShell();
            await Shell.Current.GoToAsync("//LoginPage");
        }

        private async void ButtonRegister_Clicked(object sender, EventArgs e)
        {
            var response = await _registrationService.RegisterUserAsync(_viewModel);

            if (response.IsSuccess)
            {

                await DisplayAlert("Alert", "Registration successful", "OK");

                Application.Current.MainPage = new AppShell();
                await Shell.Current.GoToAsync("//LoginPage");
            }
            else
            {
                await DisplayAlert("Alert", "Registration Failed! Please try again.", "OK");
            }
        }
    }
}