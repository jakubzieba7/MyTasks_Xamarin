using MyTasks_WebAPI.Core.DTOs;
using MyTasks_WebAPI.Core.Response;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyTasks_Xamarin.Services
{
    public class CategoryService : ICategoryService
    {
        public async Task<DataResponse<int>> AddCategoryAsync(CategoryDto category)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");

            using (var response = await App.HttpClient.PostAsync("category", stringContent))
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<DataResponse<int>>(responseContent);
            }
        }

        public async Task<Response> UpdateCategoryAsync(CategoryDto category)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");

            using (var response = await App.HttpClient.PutAsync("category", stringContent))
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Response>(responseContent);
            }
        }

        public async Task<Response> DeleteCategoryAsync(int id)
        {
            using (var response = await App.HttpClient.DeleteAsync($"category/{id}"))
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Response>(responseContent);
            }
        }

        public async Task<DataResponse<CategoryDto>> GetCategoryAsync(int id)
        {
            var json = await App.HttpClient.GetStringAsync($"category/{id}");

            return JsonConvert.DeserializeObject<DataResponse<CategoryDto>>(json);
        }

        public async Task<DataResponse<IEnumerable<CategoryDto>>> GetCategoriesAsync()
        {
            var json = await App.HttpClient.GetStringAsync("category/");

            return JsonConvert.DeserializeObject<DataResponse<IEnumerable<CategoryDto>>>(json);
        }
    }
}
