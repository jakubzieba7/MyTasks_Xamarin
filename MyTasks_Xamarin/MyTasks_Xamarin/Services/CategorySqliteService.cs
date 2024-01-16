using MyTasks_WebAPI.Core.Response;
using MyTasks_WebAPI.Core;
using MyTasks_Xamarin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MyTasks_WebAPI.Core.DTOs;
using MyTasks_Xamarin.Models.Domains;
using MyTasks_Xamarin.Models.Converters;

namespace MyTasks_Xamarin.Services
{
    public class CategorySqliteService : ICategoryService
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

        public async Task<DataResponse<int>> AddCategoryAsync(CategoryDto category)
        {
            var response = new DataResponse<int>();

            try
            {
                response.Data = await UnitOfWork.CategoryRepository.AddCategoryAsync(category.ToDao());
            }
            catch (Exception exception)
            {
                response.Errors.Add(new Error(exception.Source, exception.Message));
            }

            return response;
        }

        public async Task<Response> DeleteCategoryAsync(int id)
        {
            var response = new Response();

            try
            {
                await UnitOfWork.CategoryRepository.DeleteCategoryAsync(new Category { Id = id });
            }
            catch (Exception exception)
            {
                response.Errors.Add(new Error(exception.Source, exception.Message));
            }

            return response;
        }

        public async Task<DataResponse<CategoryDto>> GetCategoryAsync(int id)
        {
            var response = new DataResponse<CategoryDto>();

            try
            {
                response.Data = (await UnitOfWork.CategoryRepository.GetCategoryAsync(id)).ToDto();
            }
            catch (Exception exception)
            {
                response.Errors.Add(new Error(exception.Source, exception.Message));
            }

            return response;
        }

        public async Task<DataResponse<IEnumerable<CategoryDto>>> GetCategoriesAsync()
        {
            var response = new DataResponse<IEnumerable<CategoryDto>>();

            try
            {
                response.Data = (await UnitOfWork.CategoryRepository.GetCategoriesAsync()).ToDtos();
            }
            catch (Exception exception)
            {
                response.Errors.Add(new Error(exception.Source, exception.Message));
            }

            return response;
        }

        public async Task<Response> UpdateCategoryAsync(CategoryDto category)
        {
            var response = new Response();

            try
            {
                await UnitOfWork.CategoryRepository.UpdateCategoryAsync(category.ToDao());
            }
            catch (Exception exception)
            {
                response.Errors.Add(new Error(exception.Source, exception.Message));
            }

            return response;
        }
    }
}
