using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MyTasks_Xamarin.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly string _url = "https://10.0.2.2:7067/";

        public async Task<bool> RegisterUserAsync(string password, string passwordConfirmed, string email, string username)
        {
            var resp = false;

            await Task.Run(() =>
            {
                var httpClient = new HttpClient();
                var model = new ViewModels.RegistrationViewModel
                {
                    UserName = username,
                    Password = password,
                    Email = email,
                    PasswordConfirmed = passwordConfirmed,
                };

                var json = JsonConvert.SerializeObject(model);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = httpClient.PostAsync(_url + "api/Authentication/Register", httpContent);
                var result = response.GetAwaiter().GetResult();

                if (response.Result.IsSuccessStatusCode)
                    resp = true;
            });

            return resp;
        }
    }
}
