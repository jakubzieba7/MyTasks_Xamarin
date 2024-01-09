using MyTasks_WebAPI.Core.DTOs;
using MyTasks_Xamarin.Models.Domains;
using System.Collections.Generic;
using System.Linq;

namespace MyTasks_Xamarin.Models.Converters
{
    public static class TaskConverter
    {
        public static TaskDto ToDto(this Task model)
        {
            return new TaskDto()
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                IsExecuted = model.IsExecuted,
                Term = model.Term,
                CategoryId = model.CategoryId,
                UserId = model.UserId,
            };
        }

        public static IEnumerable<TaskDto> ToDtos(this IEnumerable<Task> model)
        {
            if (model == null)
                return Enumerable.Empty<TaskDto>();

            return model.Select(x => x.ToDto());
        }

        public static Task ToDao(this TaskDto model)
        {
            return new Task()
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                IsExecuted = model.IsExecuted,
                Term = model.Term,
                CategoryId = model.CategoryId,
                UserId = model.UserId,
            };
        }
    }
}
