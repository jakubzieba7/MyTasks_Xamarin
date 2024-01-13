using MyTasks_WebAPI.Core;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyTasks_Xamarin.Models.Repositories
{
    public class TaskRepository
    {
        private readonly SQLiteAsyncConnection _context;

        public TaskRepository(SQLiteAsyncConnection connection)
        {
            _context = connection;
        }

        public async Task<int> AddTaskAsync(Domains.Task task)
        {
            return await _context.InsertAsync(task);
        }

        public async Task DeleteTaskAsync(Domains.Task task)
        {
            await _context.DeleteAsync(task);
        }

        public async Task<Domains.Task> GetTaskAsync(int id)
        {
            return await _context.Table<Domains.Task>().FirstAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Domains.Task>> GetTasksAsync(PaginationFilter paginationFilter)
        {
            return await _context.Table<Domains.Task>()
                                 .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                                 .Take(paginationFilter.PageSize)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Domains.Task>> GetTasksAsync()
        {
            return await _context.Table<Domains.Task>().ToListAsync();
        }

        public async Task UpdateTaskAsync(Domains.Task task)
        {
            await _context.UpdateAsync(task);
        }
        public async Task<int> TaskCount()
        {
            return await _context.Table<Domains.Task>().CountAsync();
        }
    }
}
