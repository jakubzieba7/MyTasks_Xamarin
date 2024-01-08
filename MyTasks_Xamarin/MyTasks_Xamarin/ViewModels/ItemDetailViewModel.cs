using MyTasks_WebAPI.Core.DTOs;
using MyTasks_WebAPI.Core.Response;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyTasks_Xamarin.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private TaskDto _taskDto;

        public ItemDetailViewModel()
        {
            Title = "Podgląd zadań";
        }
        public TaskDto TaskDto
        {
            get => _taskDto;
            set => SetProperty(ref _taskDto, value);
        }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(int.Parse(value));
            }
        }

        public async void LoadItemId(int itemId)
        {
            var response = await TaskService.GetTaskAsync(itemId);

            if (!response.IsSuccess)
                await ShowErrorAlert(response);

            TaskDto = response.Data;
        }
    }
}
