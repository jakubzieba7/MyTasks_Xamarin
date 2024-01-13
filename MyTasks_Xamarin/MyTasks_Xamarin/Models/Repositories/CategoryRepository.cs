using MyTasks_Xamarin.Models.Domains;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyTasks_Xamarin.Models.Repositories
{
    public class CategoryRepository
    {
        private readonly SQLiteAsyncConnection _context;
        public CategoryRepository(SQLiteAsyncConnection context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Table<Category>().ToListAsync();
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            return await _context.Table<Category>().FirstAsync(x => x.Id == id);
        }

        public async Task<int> AddCategoryAsync(Category category)
        {
            return await _context.InsertAsync(category);
        }

        public async System.Threading.Tasks.Task UpdateAsync(Category category)
        {
            await _context.UpdateAsync(category);
        }

        public async System.Threading.Tasks.Task DeleteAsync(Category category)
        {
            await _context.DeleteAsync(category);
        }

    }
}
