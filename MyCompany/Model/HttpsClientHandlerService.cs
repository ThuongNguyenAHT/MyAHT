namespace MyCompany.Model;

public class HttpsClientHandlerService
{
    public HttpMessageHandler GetPlatformMessageHandler()
    {
#if ANDROID
        var handler = new CustomAndroidMessageHandler();
        handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
        {
            ServerUrl myServerUrl = new ServerUrl();
            if (cert != null && cert.Issuer.Equals(myServerUrl.ssl_issuer))
                return true;
            return errors == System.Net.Security.SslPolicyErrors.None;
        };
        return handler;
#elif IOS
    var handler = new NSUrlSessionHandler
    {
        TrustOverrideForUrl = IsHttpsLocalhost
        
    };
    return handler;
#else
        throw new PlatformNotSupportedException("Only Android and iOS supported.");
#endif
    }

#if ANDROID
    internal sealed class CustomAndroidMessageHandler : Xamarin.Android.Net.AndroidMessageHandler
    {
        protected override Javax.Net.Ssl.IHostnameVerifier GetSSLHostnameVerifier(Javax.Net.Ssl.HttpsURLConnection connection)
            => new CustomHostnameVerifier();

        private sealed class CustomHostnameVerifier : Java.Lang.Object, Javax.Net.Ssl.IHostnameVerifier
        {
            public bool Verify(string hostname, Javax.Net.Ssl.ISSLSession session)
            {
                ServerUrl myServerUrl = new ServerUrl();
                return Javax.Net.Ssl.HttpsURLConnection.DefaultHostnameVerifier.Verify(hostname, session) ||
                    hostname == myServerUrl.hostname && session.PeerPrincipal?.Name == myServerUrl.ssl_issuer;
            }
        }
    }
#elif IOS
    public bool IsHttpsLocalhost(NSUrlSessionHandler sender, string url, Security.SecTrust trust)
    {
        ServerUrl myServerUrl = new ServerUrl();
        if (url.Contains(myServerUrl.hostname))
        {
            return true;
        }        
        return false;
    }
#endif
}
