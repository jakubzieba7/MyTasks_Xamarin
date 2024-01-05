using MyTasks_WebAPI.Core;
using MyTasks_WebAPI.Core.DTOs;
using MyTasks_WebAPI.Core.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyTasks_Xamarin.Services
{
    public interface ITaskService
    {
        Task<DataResponse<int>> AddTaskAsync(TaskDto task);
        Task<Response> UpdateTaskAsync(TaskDto task);
        Task<Response> DeleteTaskAsync(int id);
        Task<DataResponse<TaskDto>> GetTaskAsync(int id);
        Task<DataResponse<IEnumerable<TaskDto>>> GetTasksAsync();
        Task<DataResponse<IEnumerable<TaskDto>>> GetTasksAsync(PaginationFilter paginationFilter);
    }
}
