using MyTasks_Xamarin.Services;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyTasks_Xamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        RegistrationService registrationService = new RegistrationService();
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var response = await registrationService.RegisterUserAsync(txtUsername.Text, txtEmail.Text, txtPassword.Text, txtConfirmPassword.Text);

            if (response)
            {

                await DisplayAlert("Alert", "Registration successful", "OK");

                Application.Current.MainPage = new AppShell();
                await Shell.Current.GoToAsync("//LoginPage");
            }
            else
            {
                await DisplayAlert("Alert", "Registration Failed!. Please try again.", "OK");
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Application.Current.MainPage = new AppShell();
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}