using MyTasks_WebAPI.Core.DTOs;
using MyTasks_Xamarin.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MyTasks_Xamarin.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private string _name;
        private string _description;
        private DateTime _term;
        private LookupItem _selectedCategory;
        private IEnumerable<LookupItem> _categories;

        public NewItemViewModel()
        {
            Term = DateTime.Now;
            Categories = new List<LookupItem>() { new LookupItem { Id = 1, Name = "Domyślna" } };
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(Name)
                && !String.IsNullOrWhiteSpace(Description)
                && SelectedCategory != null;
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public DateTime Term
        {
            get => _term;
            set => SetProperty(ref _term, value);
        }

        public LookupItem SelectedCategory
        {
            get => _selectedCategory;
            set => SetProperty(ref _selectedCategory, value);
        }

        public IEnumerable<LookupItem> Categories
        {
            get => _categories;
            set => SetProperty(ref _categories, value);
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
            var task = new TaskDto()
            {
                Title = Title,
                CategoryId = SelectedCategory.Id,
                Description = Description,
                Term = DateTime.Now,
            };

            var response = await TaskService.AddTaskAsync(task);

            if (!response.IsSuccess)
                await ShowErrorAlert(response);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
