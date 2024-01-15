using MyTasks_WebAPI.Core.DTOs;
using MyTasks_WebAPI.Core.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyTasks_Xamarin.Services
{
    public interface ICategoryService
    {
        Task<DataResponse<int>> AddCategoryAsync(CategoryDto category);
        Task<Response> UpdateCategoryAsync(CategoryDto category);
        Task<Response> DeleteCategoryAsync(int id);
        Task<DataResponse<CategoryDto>> GetCategoryAsync(int id);
        Task<DataResponse<IEnumerable<CategoryDto>>> GetCategoriesAsync();
    }
}
