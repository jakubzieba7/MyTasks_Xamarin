using MyTasks_Xamarin.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyTasks_Xamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new RegisterPage());
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (txtUsername.Text == "" && txtPassword.Text == "")
            {
                Application.Current.MainPage = new AppShell();
                Shell.Current.GoToAsync("//ItemsPage");
            }
            else
                DisplayAlert("Ops...", "Username or Password is incorrect!", "Ok");
        }
    }
}