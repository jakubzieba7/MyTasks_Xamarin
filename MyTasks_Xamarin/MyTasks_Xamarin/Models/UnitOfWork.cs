using MyTasks_Xamarin.Models.Domains;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyTasks_Xamarin.Models
{
    public class UnitOfWork
    {
        private readonly SQLiteAsyncConnection _context;

        public UnitOfWork(string dbPath)
        {
            _context = new SQLiteAsyncConnection(dbPath);
            _context.CreateTableAsync<Task>().Wait();
            TaskRepository = new TaskRepository(_context);
        }

        public TaskRepository TaskRepository { get; set; }
    }
}
