using MyTasks_WebAPI.Core.Response;
using MyTasks_Xamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyTasks_Xamarin.Services
{
    public interface ILoginService
    {
        Task<Response> LoginAsync(LoginViewModel model);
    }
}
