using MyTasks_WebAPI.Core.DTOs;
using Xamarin.Forms;

namespace MyTasks_Xamarin.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class CategoryDetailViewModel : BaseViewModel
    {
        private string itemId;
        private CategoryDto _categoryDto;

        public CategoryDetailViewModel()
        {
            Title = "Podgląd kategorii";
        }
        public CategoryDto Category
        {
            get => _categoryDto;
            set => SetProperty(ref _categoryDto, value);
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
            var response = await CategoryService.GetCategoryAsync(itemId);

            if (!response.IsSuccess)
                await ShowErrorAlert(response);

            Category = response.Data;
        }
    }
}
