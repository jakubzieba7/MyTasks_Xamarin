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
        private static readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri(App.BackendUrl) };

        public async Task<Response> RegisterUserAsync(RegistrationViewModel model)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            using (var response = await _httpClient.PostAsync("Authenticate", stringContent))
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Response>(responseContent);
            }
        }
    }
}
