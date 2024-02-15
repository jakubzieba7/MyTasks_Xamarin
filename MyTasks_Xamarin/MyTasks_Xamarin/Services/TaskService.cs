using MyTasks_WebAPI.Core;
using MyTasks_WebAPI.Core.DTOs;
using MyTasks_WebAPI.Core.Response;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyTasks_Xamarin.Services
{
    public class TaskService : ITaskService
    {
        public async Task<DataResponse<int>> AddTaskAsync(TaskDto task)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(task), Encoding.UTF8, "application/json");

            using (var response = await App.HttpClient.PostAsync("task", stringContent))
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<DataResponse<int>>(responseContent);
            }
        }

        public async Task<Response> UpdateTaskAsync(TaskDto task)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(task), Encoding.UTF8, "application/json");

            using (var response = await App.HttpClient.PutAsync("task", stringContent))
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Response>(responseContent);
            }
        }

        public async Task<Response> DeleteTaskAsync(int id)
        {
            using (var response = await App.HttpClient.DeleteAsync($"task/{id}"))
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Response>(responseContent);
            }
        }

        public async Task<DataResponse<TaskDto>> GetTaskAsync(int id)
        {
            var json = await App.HttpClient.GetStringAsync($"task/{id}");

            return JsonConvert.DeserializeObject<DataResponse<TaskDto>>(json);
        }

        public async Task<DataResponse<IEnumerable<TaskDto>>> GetTasksAsync()
        {
            var json = await App.HttpClient.GetStringAsync("task/");

            return JsonConvert.DeserializeObject<DataResponse<IEnumerable<TaskDto>>>(json);
        }

        public async Task<DataResponse<IEnumerable<TaskDto>>> GetTasksAsync(PaginationFilter paginationFilter)
        {
            var json = await App.HttpClient.GetStringAsync($"Task?PageNumber={paginationFilter.PageNumber}&PageSize={paginationFilter.PageSize}");

            return JsonConvert.DeserializeObject<DataResponse<IEnumerable<TaskDto>>>(json);
        }
    }
}