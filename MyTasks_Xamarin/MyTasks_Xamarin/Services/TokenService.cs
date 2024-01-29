using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Xamarin.Essentials;
using Microsoft.CSharp;
using MyTasks_WebAPI.Core.Response;

namespace MyTasks_Xamarin.Services
{
    public class TokenService
    {
        public async Task<HttpResponseMessage> GetAccessTokenAsync(string username, string password)
        {
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username",username),
                new KeyValuePair<string, string>("password",password)
            };

            var request = new HttpRequestMessage(HttpMethod.Post, App.BackendUrl + "login");

            request.Content = new FormUrlEncodedContent(keyValues);

            var client = new HttpClient();
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            JObject jdynamic = JsonConvert.DeserializeObject<dynamic>(content);
            var accessToken = jdynamic.Value<string>("access_token");
            var refreshToken = jdynamic.Value<string>("refresh_token");
            var accessTokenExpiration = jdynamic.Value<DateTime>(".expires");

            Debug.WriteLine(content);

            Preferences.Set("AccessToken", accessToken);
            Preferences.Set("RefreshToken", refreshToken);
            Preferences.Set("AccessTokenExpiration", accessTokenExpiration);

            return response;
        }
    }
}
