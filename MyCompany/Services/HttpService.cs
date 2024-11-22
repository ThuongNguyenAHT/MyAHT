using MyCompany.Model;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace MyCompany.Services;


public class HttpService
{
    public HttpClient httpClient;
    public HttpService()
    {
        //HttpClientHandler clientHandler = new HttpClientHandler();
        //clientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
        ////clientHandler.ServerCertificateCustomValidationCallback = ServerCertificateCustomValidation;
        //clientHandler.ServerCertificateCustomValidationCallback  = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; };
        //httpClient = new HttpClient(clientHandler);



#if ANDROID
        HttpsClientHandlerService handler = new HttpsClientHandlerService();
        httpClient = new HttpClient(handler.GetPlatformMessageHandler());
#elif IOS
        HttpsClientHandlerService handler = new HttpsClientHandlerService();
        httpClient = new HttpClient(handler.GetPlatformMessageHandler());
#else
        HttpClientHandler clientHandler = new HttpClientHandler();
        //clientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
        clientHandler.ServerCertificateCustomValidationCallback = ServerCertificateCustomValidation;
        //clientHandler.ServerCertificateCustomValidationCallback  = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; };
        httpClient = new HttpClient(clientHandler);
#endif

    }
    private static bool ServerCertificateCustomValidation(HttpRequestMessage requestMessage, X509Certificate2 certificate, X509Chain chain, SslPolicyErrors sslErrors)
    {
        ServerUrl myServerUrl = new ServerUrl();
        // It is possible inpect the certificate provided by server
        //Console.WriteLine($"Requested URI: {requestMessage.RequestUri}");
        //Console.WriteLine($"Effective date: {certificate.GetEffectiveDateString()}");
        //Console.WriteLine($"Exp date: {certificate.GetExpirationDateString()}");
        //Console.WriteLine($"Issuer: {certificate.Issuer}");
        //Console.WriteLine($"Subject: {certificate.Subject}");

        // Based on the custom logic it is possible to decide whether the client considers certificate valid or not
        //Console.WriteLine($"Errors: {sslErrors}");
        if (certificate.Issuer == myServerUrl.ssl_issuer)
        {
            return true;
        }
        else
        {
            return sslErrors == SslPolicyErrors.None;
        }
    }

    public static bool ValidateServerCertificate(
      object sender,
      X509Certificate certificate,
      X509Chain chain,
      SslPolicyErrors sslPolicyErrors)
    {
        if (sslPolicyErrors == SslPolicyErrors.None)
            return true;

        Console.WriteLine("Certificate error: {0}", sslPolicyErrors);

        // Do not allow this client to communicate with unauthenticated servers.
        return false;
    }
}
