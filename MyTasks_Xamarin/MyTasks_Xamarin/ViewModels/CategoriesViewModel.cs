using MyTasks_WebAPI.Core.DTOs;
using MyTasks_WebAPI.Core;
using MyTasks_Xamarin.Services;
using MyTasks_Xamarin.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyTasks_Xamarin.ViewModels
{
    public class CategoriesViewModel : BaseViewModel
    {
        public ObservableCollection<CategoryDto> Categories { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command DeleteItemCommand { get; }
        public Command<CategoryDto> ItemTapped { get; }

        public CategoriesViewModel()
        {
            Title = "Kategorie zadań";
            Categories = new ObservableCollection<CategoryDto>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<CategoryDto>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
            DeleteItemCommand = new Command<CategoryDto>(async (x) => await OnDeleteItem(x));
        }

        private async Task OnDeleteItem(CategoryDto category)
        {
            if (category == null)
                return;

            var dialog = await Shell.Current.DisplayAlert("Usuwanie!", $"Czy na pewno chcesz usunąć kategorię {category.Name}?", "Tak", "Nie");

            if (!dialog)
                return;

            var response = await CategoryService.DeleteCategoryAsync(category.Id);

            if (!response.IsSuccess)
                await ShowErrorAlert(response);

            await ExecuteLoadItemsCommand();
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                var response = await CategoryService.GetCategoriesAsync();

                if (!response.IsSuccess)
                    await ShowErrorAlert(response);

                Categories.Clear();

                foreach (var item in response.Data)
                {
                    Categories.Add(item);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Wystąpił Błąd!", ex.Message, "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewCategoryPage));
        }

        async void OnItemSelected(CategoryDto category)
        {
            if (category == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(CategoryDetailPage)}?{nameof(CategoryDetailViewModel.ItemId)}={category.Id}");
        }
    }
}

