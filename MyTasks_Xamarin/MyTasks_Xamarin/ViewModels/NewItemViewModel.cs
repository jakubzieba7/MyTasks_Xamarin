using MyTasks_WebAPI.Core.DTOs;
using MyTasks_Xamarin.Models;
using MyTasks_Xamarin.Models.Domains;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MyTasks_Xamarin.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private string _title;
        private string _description;
        private DateTime _term;
        private Category _selectedCategory;
        private IEnumerable<Category> _categories = new List<Category>() { new Category { Id = 1, Name = "Domyślna" } };

        public NewItemViewModel()
        {
            Term = DateTime.Now;
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(Title)
                && !String.IsNullOrWhiteSpace(Description)
                && SelectedCategory != null;
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
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

        public Category SelectedCategory
        {
            get => _selectedCategory;
            set => SetProperty(ref _selectedCategory, value);
        }

        public IEnumerable<Category> Categories
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
