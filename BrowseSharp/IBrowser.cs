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

        IDocument Execute(IRestRequest request);

        IDocument ExecuteAsGet(IRestRequest request, string httpMethod);

        IDocument ExecuteAsPost(IRestRequest request, string httpMethod);

        Task<IDocument> ExecuteTaskAsync(IRestRequest request, CancellationToken token);

        Task<IDocument> ExecuteTaskAsync(IRestRequest request);

        Task<IDocument> ExecuteGetTaskAsync(IRestRequest request);

        Task<IDocument> ExecuteGetTaskAsync(IRestRequest request, CancellationToken token);

        Task<IDocument> ExecutePostTaskAsync(IRestRequest request);

        Task<IDocument> ExecutePostTaskAsync(IRestRequest request, CancellationToken token);
        
        IDocument Navigate(string uri);

        IDocument Navigate(Uri uri);
        
        IDocument Navigate(string uri, Dictionary<string,string> headers);
        
        IDocument Navigate(Uri uri, Dictionary<string,string> headers);

        IDocument Navigate(string uri, Dictionary<string,string> headers, Dictionary<string,string> formData);
        
        IDocument Navigate(Uri uri, Dictionary<string,string> headers, Dictionary<string,string> formData);

        Task<IDocument> NavigateAsync(string uri);

        Task<IDocument> NavigateAsync(Uri uri);
        
        Task<IDocument> NavigateAsync(string uri, Dictionary<string,string> headers);
        
        Task<IDocument> NavigateAsync(Uri uri, Dictionary<string,string> headers);
        
        Task<IDocument> NavigateAsync(string uri, Dictionary<string,string> headers, Dictionary<string,string> formData);
        
        Task<IDocument> NavigateAsync(Uri uri, Dictionary<string,string> headers, Dictionary<string,string> formData);
        
        IDocument Submit(string uri);

        IDocument Submit(Uri uri);

        IDocument Submit(string uri, Dictionary<string,string> formData);

        IDocument Submit(Uri uri, Dictionary<string,string> formData);

        IDocument Submit(string uri, Dictionary<string, string> formData, Dictionary<string, string> headers);

        IDocument Submit(Uri uri, Dictionary<string, string> formData, Dictionary<string, string> headers);
        
        Task<IDocument> SubmitAsync(string uri);

        Task<IDocument> SubmitAsync(Uri uri);
        
        Task<IDocument> SubmitAsync(string uri, Dictionary<string,string> formData);

        Task<IDocument> SubmitAsync(Uri uri, Dictionary<string,string> formData);

        Task<IDocument> SubmitAsync(string uri, Dictionary<string, string> formData,
            Dictionary<string, string> headers);

        Task<IDocument> SubmitAsync(Uri uri, Dictionary<string, string> formData, Dictionary<string, string> headers);

    }
}