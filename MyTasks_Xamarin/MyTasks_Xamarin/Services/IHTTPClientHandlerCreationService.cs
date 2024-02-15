using System.Net.Http;

namespace MyTasks_Xamarin.Services
{
    public interface IHTTPClientHandlerCreationService
    {
        HttpClientHandler GetInsecureHandler();
    }
}
