using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyTasks_Xamarin.Services
{
    public interface IRegistrationService
    {
        Task<bool> RegisterUserAsync(string password, string passwordConfirmed, string email, string username);
    }
}
