using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Xamarin.Essentials;
using MyTasks_WebAPI.Core.Response;
using MyTasks_Xamarin.ViewModels;

namespace MyTasks_Xamarin.Services
{
    public class TokenService
    {
        public async Task<Response> GetAccessTokenAsync(LoginViewModel model)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            using (var response = await App.HttpClient.PostAsync("Authenticate/Login", stringContent))
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                JObject jdynamic = JsonConvert.DeserializeObject<dynamic>(responseContent);
                var bearer_JWT = jdynamic.Value<string>("bearer_JWT");
                var userId = jdynamic.Value<JObject>("user").Value<string>("id");
                //var user = (JObject)jdynamic["user"];     //also works
                //var userId = user["id"].ToString();

                Debug.WriteLine(responseContent);

                Preferences.Set("AccessToken", bearer_JWT);
                Preferences.Set("UserId", userId);

                return JsonConvert.DeserializeObject<Response>(responseContent);
            }
        }
    }
}
