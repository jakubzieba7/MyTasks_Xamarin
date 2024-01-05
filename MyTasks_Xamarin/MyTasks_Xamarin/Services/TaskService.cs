using MyTasks_WebAPI.Core;
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
    public class TaskService : ITaskService
    {
        private static readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri(App.BackendUrl) };

        public async Task<DataResponse<int>> AddTaskAsync(TaskDto task)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(task), Encoding.UTF8, "application/json");

            using (var response = await _httpClient.PostAsync("task", stringContent))
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<DataResponse<int>>(responseContent);
            }
        }

        public async Task<Response> UpdateTaskAsync(TaskDto task)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(task), Encoding.UTF8, "application/json");

            using (var response = await _httpClient.PutAsync("task", stringContent))
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Response>(responseContent);
            }
        }

        public async Task<Response> DeleteTaskAsync(int id)
        {
            using (var response = await _httpClient.DeleteAsync($"task/{id}"))
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Response>(responseContent);
            }
        }

        public async Task<DataResponse<TaskDto>> GetTaskAsync(int id)
        {
            var json = await _httpClient.GetStringAsync($"task/{id}");

            return JsonConvert.DeserializeObject<DataResponse<TaskDto>>(json);
        }

        public async Task<DataResponse<IEnumerable<TaskDto>>> GetTasksAsync()
        {
            var json = await _httpClient.GetStringAsync("task/");

            return JsonConvert.DeserializeObject<DataResponse<IEnumerable<TaskDto>>>(json);
        }

        public async Task<DataResponse<IEnumerable<TaskDto>>> GetTasksAsync(PaginationFilter paginationFilter)
        {
            var json = await _httpClient.GetStringAsync($"Task?PageNumber={paginationFilter.PageNumber}&PageSize={paginationFilter.PageSize}");

            return JsonConvert.DeserializeObject<DataResponse<IEnumerable<TaskDto>>>(json);
        }
    }
}