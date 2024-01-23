using MyTasks_Xamarin.Services;
using MyTasks_Xamarin.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyTasks_Xamarin
{
    public partial class App : Application
    {
        public static string BackendUrl = "http://10.0.2.2:88/api/";
        public App()
        {
            InitializeComponent();

            DependencyService.Register<TaskService>();
            DependencyService.Register<CategoryService>();
            DependencyService.Register<RegistrationService>();
            //DependencyService.Register<TaskSqliteService>();
            //DependencyService.Register<CategorySqliteService>();
            //MainPage = new AppShell();
            MainPage = new NavigationPage(new LoginPage());
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
