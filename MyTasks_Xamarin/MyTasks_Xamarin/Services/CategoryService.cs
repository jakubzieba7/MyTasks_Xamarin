using MyTasks_WebAPI.Core.DTOs;
using MyTasks_WebAPI.Core.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyTasks_Xamarin.Services
{
    public class CategoryService : ICategoryService
    {
        private static readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri(App.BackendUrl) };

        public async Task<DataResponse<int>> AddCategoryAsync(CategoryDto category)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");

            using (var response = await _httpClient.PostAsync("category", stringContent))
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<DataResponse<int>>(responseContent);
            }
        }

        public async Task<Response> UpdateCategoryAsync(CategoryDto category)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");

            using (var response = await _httpClient.PutAsync("category", stringContent))
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Response>(responseContent);
            }
        }

        public async Task<Response> DeleteCategoryAsync(int id)
        {
            using (var response = await _httpClient.DeleteAsync($"category/{id}"))
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Response>(responseContent);
            }
        }

        public async Task<DataResponse<CategoryDto>> GetCategoryAsync(int id)
        {
            var json = await _httpClient.GetStringAsync($"category/{id}");

            return JsonConvert.DeserializeObject<DataResponse<CategoryDto>>(json);
        }

        public async Task<DataResponse<IEnumerable<CategoryDto>>> GetCategoriesAsync()
        {
            var json = await _httpClient.GetStringAsync("category/");

            return JsonConvert.DeserializeObject<DataResponse<IEnumerable<CategoryDto>>>(json);
        }
    }
}
