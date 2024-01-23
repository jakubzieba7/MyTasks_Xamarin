﻿using MyTasks_WebAPI.Core.Response;
using MyTasks_Xamarin.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyTasks_Xamarin.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public ITaskService TaskService => DependencyService.Get<ITaskService>();
        public ICategoryService CategoryService => DependencyService.Get<ICategoryService>();
        public IRegistrationService RegistrationService => DependencyService.Get<IRegistrationService>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        protected async Task ShowErrorAlert(Response response)
        {
            await Shell.Current.DisplayAlert("Wystąpił Błąd!", string.Join(". ", response.Errors.Select(x => x.Message)), "Ok");
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
