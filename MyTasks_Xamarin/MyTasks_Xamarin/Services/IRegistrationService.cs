using MyTasks_WebAPI.Core.DTOs;
using MyTasks_WebAPI.Core.Response;
using MyTasks_Xamarin.ViewModels;
using System.Threading.Tasks;

namespace MyTasks_Xamarin.Services
{
    public interface IRegistrationService
    {
        Task<Response> RegisterUserAsync(RegistrationViewModel model);
    }
}
