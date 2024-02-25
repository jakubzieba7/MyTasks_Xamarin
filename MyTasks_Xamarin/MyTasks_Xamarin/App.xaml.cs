using MyTasks_Xamarin.Services;
using MyTasks_Xamarin.Views;
using System;
using System.Net.Http;
using System.Net;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MyTasks_Xamarin
{
    public partial class App : Application
    {
        public static string BackendUrl = DeviceInfo.Platform == DevicePlatform.Android? "https://10.0.2.2:88/api/" : "https://localhost:88/api/";
        public static HttpClient HttpClient;
        public static string UserId;

        public App()
        {
            InitializeComponent();

            DependencyService.Register<TaskService>();
            DependencyService.Register<CategoryService>();
            DependencyService.Register<RegistrationService>();
            DependencyService.Register<LoginService>();
            //DependencyService.Register<TaskSqliteService>();
            //DependencyService.Register<CategorySqliteService>();
            //MainPage = new AppShell();
            MainPage = new NavigationPage(new LoginPage());
            //HttpClient = httpClient;

            var accessToken = Preferences.Get("AccessToken", "default_value");
            UserId = Preferences.Get("UserId", "default_value");

            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    HttpClient = new HttpClient(DependencyService.Get<IHTTPClientHandlerCreationService>().GetInsecureHandler()) { BaseAddress = new Uri(BackendUrl) };
                    break;
                default:
                    ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                    HttpClient = new HttpClient(new HttpClientHandler()) { BaseAddress = new Uri(App.BackendUrl) };
                    break;
            }

            HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
