using MyTasks_WebAPI.Core;
using MyTasks_WebAPI.Core.DTOs;
using MyTasks_WebAPI.Core.Response;
using MyTasks_Xamarin.Models;
using MyTasks_Xamarin.Models.Domains;
using MyTasks_Xamarin.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyTasks_Xamarin.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private string _title;
        private string _categoryName;
        private string _description;
        private DateTime _term;
        private CategoryDto _selectedCategory;
        private Category _selectedCategorySQLite;
        private IEnumerable<Category> _categoriesSQLite;
        private IEnumerable<CategoryDto> _categories;

        public NewItemViewModel()
        {
            _categories = CategoryList().Data;
            _categoriesSQLite = CategorySQLiteList();
            Term = DateTime.Now;
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private async Task<IEnumerable<Category>> CategorySQLiteListAsync()
        {
            var categoriesList = await CategorySqliteService.UnitOfWork.CategoryRepository.GetCategoriesAsync();

            return categoriesList;
        }

        private IEnumerable<Category> CategorySQLiteList()
        {
            var categoryList = new Func<Task<IEnumerable<Category>>>(CategorySQLiteListAsync);
            var asyncResult = categoryList.BeginInvoke(null, null);
            asyncResult.AsyncWaitHandle.WaitOne();
            var categoriesResult = categoryList.EndInvoke(asyncResult);

            return categoriesResult.Result;
        }

        private async Task<DataResponse<IEnumerable<CategoryDto>>> CategoryListAsync()
        {
            var categoriesList = await CategoryService.GetCategoriesAsync();

            return categoriesList;
        }

        private DataResponse<IEnumerable<CategoryDto>> CategoryList()
        {
            var categoryList = new Func<Task<DataResponse<IEnumerable<CategoryDto>>>>(CategoryListAsync);
            var asyncResult = categoryList.BeginInvoke(null, null);
            asyncResult.AsyncWaitHandle.WaitOne();
            var categoriesResult = categoryList.EndInvoke(asyncResult);

            return categoriesResult.Result;
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

        public string Name
        {
            get => _categoryName;
            set => SetProperty(ref _categoryName, value);
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

        public Category SelectedCategorySQLite
        {
            get => _selectedCategorySQLite;
            set => SetProperty(ref _selectedCategorySQLite, value);
        }

        public CategoryDto SelectedCategory
        {
            get => _selectedCategory;
            set => SetProperty(ref _selectedCategory, value);
        }

        public IEnumerable<Category> CategoriesSQLite
        {
            get => _categoriesSQLite;
            set => SetProperty(ref _categoriesSQLite, value);
        }

        public IEnumerable<CategoryDto> Categories
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
                //CategoryId= SelectedCategorySQLite.Id,
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
