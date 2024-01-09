using MyTasks_Xamarin.Models.Domains;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyTasks_Xamarin.Models
{
    public class TaskRepository
    {
        private readonly SQLiteAsyncConnection _context;

        public TaskRepository(SQLiteAsyncConnection connection)
        {
            _context = connection;
        }

        public Task<int> AddTaskAsync(Task task)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTaskAsync(Task task)
        {
            throw new NotImplementedException();
        }

        public Task GetTaskAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTaskAsync(Task task)
        {
            throw new NotImplementedException();
        }
    }
}
