using MyTasks_WebAPI.Core.DTOs;
using MyTasks_Xamarin.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyTasks_Xamarin.Models.Converters
{
    public static class CategoryConverter
    {
        public static CategoryDto ToDto(this Category model)
        {
            return new CategoryDto()
            {
                Id = model.Id,
                Name = model.Name,
            };
        }

        public static IEnumerable<CategoryDto> ToDtos(this IEnumerable<Category> model)
        {
            if (model == null)
                return Enumerable.Empty<CategoryDto>();

            return model.Select(x => x.ToDto());
        }

        public static Category ToDao(this CategoryDto model)
        {
            return new Category()
            {
                Id = model.Id,
                Name = model.Name,
            };
        }
    }
}
