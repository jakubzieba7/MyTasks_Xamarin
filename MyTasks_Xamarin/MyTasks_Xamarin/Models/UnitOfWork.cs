using MyTasks_Xamarin.Models.Domains;
using MyTasks_Xamarin.Models.Repositories;
using SQLite;

namespace MyTasks_Xamarin.Models
{
    public class UnitOfWork
    {
        private readonly SQLiteAsyncConnection _context;

        public UnitOfWork(string dbPath)
        {
            _context = new SQLiteAsyncConnection(dbPath);
            _context.CreateTableAsync<Task>().Wait();
            _context.CreateTableAsync<Category>().Wait();
            TaskRepository = new TaskRepository(_context);
            CategoryRepository = new CategoryRepository(_context);
        }

        public TaskRepository TaskRepository { get; set; }
        public CategoryRepository CategoryRepository { get; set; }
    }
}
