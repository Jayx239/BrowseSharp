using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;

namespace BrowseSharp
{
    public interface IBrowser
    {
        
        CookieContainer CookieContainer { get; set; }

    bool AutomaticDecompression { get; set; }

    int? MaxRedirects { get; set; }

    string UserAgent { get; set; }

    int Timeout { get; set; }

    int ReadWriteTimeout { get; set; }

    bool UseSynchronizationContext { get; set; }

    IAuthenticator Authenticator { get; set; }

    Uri BaseUrl { get; set; }

    Encoding Encoding { get; set; }

    bool PreAuthenticate { get; set; }

    IList<Parameter> DefaultParameters { get; }

    string BaseHost { get; set; }

    
    IRestResponse<T> Deserialize<T>(IRestResponse response);

    byte[] DownloadData(IRestRequest request);

    X509CertificateCollection ClientCertificates { get; set; }

    IWebProxy Proxy { get; set; }

    RequestCachePolicy CachePolicy { get; set; }

    bool Pipelined { get; set; }

    bool FollowRedirects { get; set; }

    Uri BuildUri(IRestRequest request);

    RemoteCertificateValidationCallback RemoteCertificateValidationCallback { get; set; }

    void AddHandler(string contentType, IDeserializer deserializer);

    void RemoveHandler(string contentType);

    void ClearHandlers();

    
    
        //
        
        IDocument Execute(IRestRequest request);
        
        IDocument ExecuteAsGet(IRestRequest request, string httpMethod);

        IDocument ExecuteAsPost(IRestRequest request, string httpMethod);

        
        Task<IDocument> ExecuteTaskAsync(IRestRequest request, CancellationToken token);

        Task<IDocument> ExecuteTaskAsync(IRestRequest request);


        Task<IDocument> ExecuteGetTaskAsync(IRestRequest request);

        Task<IDocument> ExecuteGetTaskAsync(IRestRequest request, CancellationToken token);


        Task<IDocument> ExecutePostTaskAsync(IRestRequest request);

        Task<IDocument> ExecutePostTaskAsync(IRestRequest request, CancellationToken token);

    }
}