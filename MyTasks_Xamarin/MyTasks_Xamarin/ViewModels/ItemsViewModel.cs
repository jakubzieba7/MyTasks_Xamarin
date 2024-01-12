using MyTasks_WebAPI.Core;
using MyTasks_WebAPI.Core.DTOs;
using MyTasks_Xamarin.Views;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyTasks_Xamarin.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        PaginationFilter paginationFilter = new PaginationFilter();
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
                var response = await TaskService.GetTasksAsync(paginationFilter);

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

        private Command firstPageCommand;

        public ICommand FirstPageCommand
        {
            get
            {
                if (firstPageCommand == null)
                {
                    firstPageCommand = new Command(FirstPage);
                }

                return firstPageCommand;
            }
        }

        private void FirstPage()
        {
        }

        private Command previousPageCommand;

        public ICommand PreviousPageCommand
        {
            get
            {
                if (previousPageCommand == null)
                {
                    previousPageCommand = new Command(PreviousPage);
                }

                return previousPageCommand;
            }
        }

        private void PreviousPage()
        {
        }

        private Command nextPageCommand;

        public ICommand NextPageCommand
        {
            get
            {
                if (nextPageCommand == null)
                {
                    nextPageCommand = new Command(NextPage);
                }

                return nextPageCommand;
            }
        }

        private void NextPage()
        {
        }

        private Command lastPageCommand;

        public ICommand LastPageCommand
        {
            get
            {
                if (lastPageCommand == null)
                {
                    lastPageCommand = new Command(LastPage);
                }

                return lastPageCommand;
            }
        }

        private void LastPage()
        {
        }
    }
}