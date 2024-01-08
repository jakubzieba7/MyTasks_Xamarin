using MyTasks_WebAPI.Core.DTOs;
using MyTasks_Xamarin.Views;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyTasks_Xamarin.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<TaskDto> Tasks { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command DeleteItemCommand { get; }
        public Command<TaskDto> ItemTapped { get; }

        public ItemsViewModel()
        {
            Title = "Zadania do wykonania";
            Tasks = new ObservableCollection<TaskDto>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<TaskDto>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
            DeleteItemCommand = new Command<TaskDto>(async (x) => await OnDeleteItem(x));
        }

        private async Task OnDeleteItem(TaskDto task)
        {
            if (task == null)
                return;

            var dialog = await Shell.Current.DisplayAlert("Usuwanie!", $"Czy na pewno chcesz usunąć operację {task.Title}?", "Tak", "Nie");

            if (!dialog)
                return;

            var response = await TaskService.DeleteTaskAsync(task.Id);

            if (!response.IsSuccess)
                await ShowErrorAlert(response);

            await ExecuteLoadItemsCommand();
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                var response = await TaskService.GetTasksAsync();

                if (!response.IsSuccess)
                    await ShowErrorAlert(response);

                Tasks.Clear();

                foreach (var item in response.Data)
                {
                    Tasks.Add(item);
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
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(TaskDto task)
        {
            if (task == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={task.Id}");
        }
    }
}