using Android.Net;
using Javax.Net.Ssl;
using System;
using System.Net.Http;
using Xamarin.Android.Net;
using Xamarin.Forms;
using static MyTasks_Xamarin.Droid.HTTPClientHandler;
[assembly: Dependency(typeof(HTTPClientHandlerCreationService_Android))]
namespace MyTasks_Xamarin.Droid
{
    public class HTTPClientHandler
    {
        public class HTTPClientHandlerCreationService_Android : Services.IHTTPClientHandlerCreationService
        {
            public HttpClientHandler GetInsecureHandler()
            {
                return new IgnoreSSLClientHandler();
            }
        }

        internal class IgnoreSSLClientHandler : AndroidClientHandler
        {
            [Obsolete]
            protected override SSLSocketFactory ConfigureCustomSSLSocketFactory(HttpsURLConnection connection)
            {
                return SSLCertificateSocketFactory.GetInsecure(1000, null);
            }

            protected override IHostnameVerifier GetSSLHostnameVerifier(HttpsURLConnection connection)
            {
                return new IgnoreSSLHostnameVerifier();
            }
        }

        internal class IgnoreSSLHostnameVerifier : Java.Lang.Object, IHostnameVerifier
        {
            public bool Verify(string hostname, ISSLSession session)
            {
                return true;
            }
        }
    }
}