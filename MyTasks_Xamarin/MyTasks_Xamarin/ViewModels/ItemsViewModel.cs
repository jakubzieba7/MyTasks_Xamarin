using MyTasks_WebAPI.Core;
using MyTasks_WebAPI.Core.DTOs;
using MyTasks_WebAPI.Core.Response;
using MyTasks_Xamarin.Services;
using MyTasks_Xamarin.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyTasks_Xamarin.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        PaginationFilter _paginationFilter = new PaginationFilter();
        public ObservableCollection<TaskDto> Tasks { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command DeleteItemCommand { get; }
        public Command<TaskDto> ItemTapped { get; }
        public Command FirstPageCommand { get; }

        public Command PreviousPageCommand { get; }

        public Command NextPageCommand { get; }

        public Command LastPageCommand { get; }

        public ItemsViewModel()
        {
            Title = "Zadania do wykonania";
            Tasks = new ObservableCollection<TaskDto>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<TaskDto>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
            DeleteItemCommand = new Command<TaskDto>(async (x) => await OnDeleteItem(x));

            FirstPageCommand = new Command(async () => await OnFirstPage());
            PreviousPageCommand = new Command(async () => await OnPreviousPage(), CanPreviousPage);
            NextPageCommand = new Command(async () => await OnNextPage(), CanNextPage);
            //NextPageCommand = new Command(async () => await OnNextPage(), CanNextPageSQLite);
            LastPageCommand = new Command(async () => await OnLastPage());
            //LastPageCommand = new Command(async () => await OnLastPageSQLite());
        }

        private async Task<bool> CanNextPageAsync()
        {
            var totalRecords = await TaskSqliteService.UnitOfWork.TaskRepository.TaskCount();
            var totalPages = (double)totalRecords / (double)_paginationFilter.PageSize;
            var roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));

            return _paginationFilter.PageNumber < roundedTotalPages;
        }


        private bool CanNextPageSQLite()
        {
            var nextPageCaller = new Func<Task<bool>>(CanNextPageAsync);
            var asyncResult = nextPageCaller.BeginInvoke(null, null);
            asyncResult.AsyncWaitHandle.WaitOne();
            var nextPageResult = nextPageCaller.EndInvoke(asyncResult);

            return nextPageResult.Result;
        }

        private bool CanNextPage()
        {
            var totalRecords = TaskList().Data.Count();
            var totalPages = (double)totalRecords / (double)_paginationFilter.PageSize;
            var roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));

            return _paginationFilter.PageNumber < roundedTotalPages;
        }

        private bool CanPreviousPage()
        {
            return _paginationFilter.PageNumber > 1;
        }

        private async Task<DataResponse<IEnumerable<TaskDto>>> TaskListAsync()
        {
            var taskList = await TaskService.GetTasksAsync();

            return taskList;
        }

        private DataResponse<IEnumerable<TaskDto>> TaskList()
        {
            var taskList = new Func<Task<DataResponse<IEnumerable<TaskDto>>>>(TaskListAsync);
            var asyncResult = taskList.BeginInvoke(null, null);
            asyncResult.AsyncWaitHandle.WaitOne();
            var categoriesResult = taskList.EndInvoke(asyncResult);

            return categoriesResult.Result;
        }

        private async Task<int> LastPageAsync()
        {
            var totalRecords = await TaskSqliteService.UnitOfWork.TaskRepository.TaskCount();
            var totalPages = (double)totalRecords / (double)_paginationFilter.PageSize;
            var roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));

            return roundedTotalPages;
        }

        private async Task OnLastPageSQLite()
        {
            var lastPageCaller = new Func<Task<int>>(LastPageAsync);
            var asyncResult = lastPageCaller.BeginInvoke(null, null);
            asyncResult.AsyncWaitHandle.WaitOne();
            var lastPageResult = lastPageCaller.EndInvoke(asyncResult);

            _paginationFilter.PageNumber = lastPageResult.Result;

            await ExecuteLoadItemsCommand();
        }

        private async Task OnLastPage()
        {
            var totalRecords = TaskList().Data.Count();
            var totalPages = (double)totalRecords / (double)_paginationFilter.PageSize;
            var roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));

            _paginationFilter.PageNumber = roundedTotalPages;

            await ExecuteLoadItemsCommand();
        }

        private async Task OnNextPage()
        {
            _paginationFilter.PageNumber++;
            await ExecuteLoadItemsCommand();
        }

        private async Task OnPreviousPage()
        {
            _paginationFilter.PageNumber--;
            await ExecuteLoadItemsCommand();
        }

        private async Task OnFirstPage()
        {
            _paginationFilter.PageNumber = 1;
            await ExecuteLoadItemsCommand();
        }

        private async Task OnDeleteItem(TaskDto task)
        {
            if (task == null)
                return;

            var dialog = await Shell.Current.DisplayAlert("Usuwanie!", $"Czy na pewno chcesz usunąć zadanie {task.Title}?", "Tak", "Nie");

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
                var response = await TaskService.GetTasksAsync(_paginationFilter);

                if (!response.IsSuccess)
                    await ShowErrorAlert(response);

                Tasks.Clear();

                foreach (var item in response.Data)
                {
                    Tasks.Add(item);
                }

                PreviousPageCommand.ChangeCanExecute();
                NextPageCommand.ChangeCanExecute();
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