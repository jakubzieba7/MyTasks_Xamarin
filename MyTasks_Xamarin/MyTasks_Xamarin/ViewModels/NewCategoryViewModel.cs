using MyTasks_WebAPI.Core.DTOs;
using MyTasks_Xamarin.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MyTasks_Xamarin.ViewModels
{
    public class NewCategoryViewModel : BaseViewModel
    {
        private string _name;

        public NewCategoryViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(Name);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
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
            var category = new CategoryDto()
            {
                Name = Name,
                UserId = App.UserId,
            };

            var response = await CategoryService.AddCategoryAsync(category);

            if (!response.IsSuccess)
                await ShowErrorAlert(response);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
