using MyTasks_WebAPI.Core.Response;
using MyTasks_Xamarin.ViewModels;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyTasks_Xamarin.Services
{
    public class RegistrationService : IRegistrationService
    {
        
        public async Task<Response> RegisterUserAsync(RegistrationViewModel model)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            using (var response = await  App.HttpClient.PostAsync("Authenticate/Register", stringContent))
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Response>(responseContent);
            }
        }
    }
}
