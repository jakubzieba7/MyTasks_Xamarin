using MyTasks_WebAPI.Core;
using MyTasks_WebAPI.Core.DTOs;
using MyTasks_WebAPI.Core.Response;
using MyTasks_Xamarin.Models;
using MyTasks_Xamarin.Models.Converters;
using MyTasks_Xamarin.Models.Domains;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MyTasks_Xamarin.Services
{
    public class TaskSqliteService : ITaskService
    {
        private static UnitOfWork _unitOfWork;

        public static UnitOfWork UnitOfWork
        {
            get
            {
                if (_unitOfWork == null)
                {
                    _unitOfWork = new UnitOfWork(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyTasksSQLite.db3"));
                }
                return _unitOfWork;
            }
        }
        public async Task<DataResponse<int>> ITaskService.AddTaskAsync(TaskDto task)
        {
            var response = new DataResponse<int>();

            try
            {
                response.Data = await UnitOfWork.TaskRepository.AddTaskAsync(task.ToDao());
            }
            catch (Exception exception)
            {
                response.Errors.Add(new Error(exception.Source, exception.Message));
            }

            return response;
        }

        public async Task<Response> ITaskService.DeleteTaskAsync(int id)
        {
            var response = new Response();

            try
            {
                await UnitOfWork.TaskRepository.DeleteTaskAsync(new Models.Domains.Task { Id = id });
            }
            catch (Exception exception)
            {
                response.Errors.Add(new Error(exception.Source, exception.Message));
            }

            return response;
        }

        public async Task<DataResponse<TaskDto>> ITaskService.GetTaskAsync(int id)
        {
            var response = new DataResponse<TaskDto>();

            try
            {
                response.Data = (await UnitOfWork.TaskRepository.GetTaskAsync(id)).ToDto();
            }
            catch (Exception exception)
            {
                response.Errors.Add(new Error(exception.Source, exception.Message));
            }

            return response;
        }

        public async Task<DataResponse<IEnumerable<TaskDto>>> ITaskService.GetTasksAsync()
        {
            var response = new DataResponse<IEnumerable<TaskDto>>();

            try
            {
                response.Data = (await UnitOfWork.TaskRepository.GetTasksAsync()).ToDtos();
            }
            catch (Exception exception)
            {
                response.Errors.Add(new Error(exception.Source, exception.Message));
            }

            return response;
        }

        public async Task<DataResponse<IEnumerable<TaskDto>>> ITaskService.GetTasksAsync(PaginationFilter paginationFilter)
        {
            var response = new DataResponse<IEnumerable<TaskDto>>();

            try
            {
                response.Data = (await UnitOfWork.TaskRepository.GetTasksAsync(paginationFilter)).ToDtos();
            }
            catch (Exception exception)
            {
                response.Errors.Add(new Error(exception.Source, exception.Message));
            }

            return response;
        }

        public async Task<Response> ITaskService.UpdateTaskAsync(TaskDto task)
        {
            var response = new Response();

            try
            {
                await UnitOfWork.TaskRepository.UpdateTaskAsync(task.ToDao());
            }
            catch (Exception exception)
            {
                response.Errors.Add(new Error(exception.Source, exception.Message));
            }

            return response;
        }
    }
}
