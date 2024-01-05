﻿using MyTasks_Xamarin.Services;
using MyTasks_Xamarin.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyTasks_Xamarin
{
    public partial class App : Application
    {
        public static string BackendUrl = "";
        public App()
        {
            InitializeComponent();

            DependencyService.Register<TaskService>();
            MainPage = new AppShell();
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
