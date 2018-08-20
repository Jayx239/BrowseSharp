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
    /// <summary>
    /// Browser interface for BrowseSharp
    /// </summary>
    public interface IBrowser
    {
        /// <summary>
        /// CookieContainer wrapper for internal restsharp client
        /// </summary>
        CookieContainer CookieContainer { get; set; }

        /// <summary>
        /// AutomaticDecompression attribute wrapper for internal restsharp client
        /// </summary>
        bool AutomaticDecompression { get; set; }

        /// <summary>
        /// MaxRedirects attribute wrapper for internal restsharp client
        /// </summary>
        int? MaxRedirects { get; set; }

        /// <summary>
        /// UserAgent attribute wrapper for internal restsharp client
        /// </summary>
        string UserAgent { get; set; }

        /// <summary>
        /// Timeout attribute wrapper for internal restsharp client
        /// </summary>
        int Timeout { get; set; }

        /// <summary>
        /// ReadWriteTimeout attribute wrapper for internal restsharp client
        /// </summary>
        int ReadWriteTimeout { get; set; }

        /// <summary>
        /// UseSynchronizationContext attribute wrapper for internal restsharp client
        /// </summary>
        bool UseSynchronizationContext { get; set; }

        /// <summary>
        /// Authenticator attribute wrapper for internal restsharp client
        /// </summary>
        IAuthenticator Authenticator { get; set; }

        /// <summary>
        /// BaseUrl attribute wrapper for internal restsharp client
        /// Url used in request
        /// </summary>
        Uri BaseUrl { get; set; }

        /// <summary>
        /// Encoding attribute wrapper for internal restsharp client
        /// Encoding type for request
        /// </summary>
        Encoding Encoding { get; set; }

        /// <summary>
        /// PreAuthenticate attribute wrapper for internal restsharp client
        /// </summary>
        bool PreAuthenticate { get; set; }

        /// <summary>
        /// DefaultParameters attribute wrapper for internal restsharp client
        /// </summary>
        IList<Parameter> DefaultParameters { get; }

        /// <summary>
        /// BaseHost attribute wrapper for internal restsharp client
        /// </summary>
        string BaseHost { get; set; }

        /// <summary>
        /// Deserialize method wrapper for internal restsharp client
        /// </summary>
        /// <param name="response"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IRestResponse<T> Deserialize<T>(IRestResponse response);

        /// <summary>
        /// DownloadData method wrapper for internal restsharp client
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        byte[] DownloadData(IRestRequest request);

        /// <summary>
        /// ClientCertificates wrapper for internal restsharp client
        /// </summary>
        X509CertificateCollection ClientCertificates { get; set; }

        /// <summary>
        /// Proxy attribute wrapper for internal restsharp client
        /// </summary>
        IWebProxy Proxy { get; set; }

        /// <summary>
        /// RequestCachePolicy attribute wrapper for internal restsharp client
        /// </summary>
        RequestCachePolicy CachePolicy { get; set; }

        /// <summary>
        /// Pipelined attribute wrapper for internal restsharp client
        /// </summary>
        bool Pipelined { get; set; }

        /// <summary>
        /// FollowRedirects attribute wrapper for internal restsharp client
        /// </summary>
        bool FollowRedirects { get; set; }

        /// <summary>
        /// BuildUri Method wrapper for internal restsharp client
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Uri BuildUri(IRestRequest request);

        /// <summary>
        /// RemoteCertificateValidationCallback method wrapper for internal restsharp client
        /// </summary>
        RemoteCertificateValidationCallback RemoteCertificateValidationCallback { get; set; }

        /// <summary>
        /// AddHandler method wrapper for internal restsharp client
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="deserializer"></param>
        void AddHandler(string contentType, IDeserializer deserializer);

        /// <summary>
        /// RemoveHandler method wrapper for internal restsharp client
        /// </summary>
        /// <param name="contentType"></param>
        void RemoveHandler(string contentType);

        /// <summary>
        /// ClearHandlers method wrapper for internal restsharp client
        /// </summary>
        void ClearHandlers();

        /// <summary>
        /// Execute method that creates a document from an http request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IDocument Execute(IRestRequest request);

        /// <summary>
        /// Execute method that creates a document from an http get request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="httpMethod"></param>
        /// <returns></returns>
        IDocument ExecuteAsGet(IRestRequest request, string httpMethod);

        /// <summary>
        /// Execute method that creates a document from an http post request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="httpMethod"></param>
        /// <returns></returns>
        IDocument ExecuteAsPost(IRestRequest request, string httpMethod);

        /// <summary>
        /// Execute method that creates a document from an asynchronous http request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IDocument> ExecuteTaskAsync(IRestRequest request, CancellationToken token);

        /// <summary>
        /// Execute method that creates a document from an asynchronous http request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IDocument> ExecuteTaskAsync(IRestRequest request);

        /// <summary>
        /// Execute method that creates a document from an asynchronous http get request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IDocument> ExecuteGetTaskAsync(IRestRequest request);

        /// <summary>
        /// Execute method that creates a document from an asynchronous http get request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IDocument> ExecuteGetTaskAsync(IRestRequest request, CancellationToken token);

        /// <summary>
        /// Execute method that creates a document from an asynchronous http post request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IDocument> ExecutePostTaskAsync(IRestRequest request);

        /// <summary>
        /// Execute method that creates a document from an asynchronous http post request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IDocument> ExecutePostTaskAsync(IRestRequest request, CancellationToken token);
        
        /// <summary>
        /// Method that makes a get request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        IDocument Navigate(string uri);

        /// <summary>
        /// Method that makes a get request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        IDocument Navigate(Uri uri);
        
        /// <summary>
        /// Method that makes a get request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        IDocument Navigate(string uri, Dictionary<string,string> headers);
        
        /// <summary>
        /// Method that makes a get request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        IDocument Navigate(Uri uri, Dictionary<string,string> headers);

        /// <summary>
        /// Method that makes a get request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="headers"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        IDocument Navigate(string uri, Dictionary<string,string> headers, Dictionary<string,string> formData);
        
        /// <summary>
        /// Method that makes a get request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="headers"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        IDocument Navigate(Uri uri, Dictionary<string,string> headers, Dictionary<string,string> formData);

        /// <summary>
        /// Method that makes an asynchronous get request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        Task<IDocument> NavigateAsync(string uri);

        /// <summary>
        /// Method that makes an asynchronous get request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        Task<IDocument> NavigateAsync(Uri uri);
        
        /// <summary>
        /// Method that makes an asynchronous get request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<IDocument> NavigateAsync(string uri, Dictionary<string,string> headers);
        
        /// <summary>
        /// Method that makes an asynchronous get request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<IDocument> NavigateAsync(Uri uri, Dictionary<string,string> headers);
        
        /// <summary>
        /// Method that makes an asynchronous get request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="headers"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        Task<IDocument> NavigateAsync(string uri, Dictionary<string,string> headers, Dictionary<string,string> formData);
        
        /// <summary>
        /// Method that makes an asynchronous get request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="headers"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        Task<IDocument> NavigateAsync(Uri uri, Dictionary<string,string> headers, Dictionary<string,string> formData);
        
        /// <summary>
        /// Method that makes a post request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        IDocument Submit(string uri);

        /// <summary>
        /// Method that makes a post request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        IDocument Submit(Uri uri);

        /// <summary>
        /// Method that makes a post request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        IDocument Submit(string uri, Dictionary<string,string> formData);

        /// <summary>
        /// Method that makes a post request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        IDocument Submit(Uri uri, Dictionary<string,string> formData);

        /// <summary>
        /// Method that makes a post request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="formData"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        IDocument Submit(string uri, Dictionary<string, string> formData, Dictionary<string, string> headers);

        /// <summary>
        /// Method that makes a post request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="formData"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        IDocument Submit(Uri uri, Dictionary<string, string> formData, Dictionary<string, string> headers);
        
        /// <summary>
        /// Method that makes an asynchronous post request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        Task<IDocument> SubmitAsync(string uri);

        /// <summary>
        /// Method that makes an asynchronous post request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        Task<IDocument> SubmitAsync(Uri uri);
        
        /// <summary>
        /// Method that makes an asynchronous post request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        Task<IDocument> SubmitAsync(string uri, Dictionary<string,string> formData);

        /// <summary>
        /// Method that makes an asynchronous post request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        Task<IDocument> SubmitAsync(Uri uri, Dictionary<string,string> formData);

        /// <summary>
        /// Method that makes an asynchronous post request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="formData"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<IDocument> SubmitAsync(string uri, Dictionary<string, string> formData,
            Dictionary<string, string> headers);

        /// <summary>
        /// Method that makes an asynchronous post request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="formData"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<IDocument> SubmitAsync(Uri uri, Dictionary<string, string> formData, Dictionary<string, string> headers);

        /// <summary>
        /// Contains all previous documents stored for each previous request
        /// </summary>
        List<IDocument> History { get; }
        
        /// <summary>
        /// Stores the forward history when the back method is called 
        /// </summary>
        List<IDocument> ForwardHistory { get; }
        
        /// <summary>
        /// Clears browse history by re-initializing Documents
        /// </summary>
        void ClearHistory();

        /// <summary>
        /// Clears forward history
        /// </summary>
        void ClearForwardHistory();
        
        /// <summary>
        /// Method for navigating to last browser state by re-issuing the previous request
        /// </summary>
        IDocument Back();

        /// <summary>
        /// Method for navigating to last browser state
        /// </summary>
        /// <param name="useCache">Determines whether to re-issue request or reload last document</param>
        IDocument Back(bool useCache);

        /// <summary>
        /// Method for navigating to last browser state by re-issuing the previous request asynchronously
        /// </summary>
        Task<IDocument> BackAsync();
        
        /// <summary>
        /// Method for navigating to last browser state asynchronously
        /// </summary>
        /// <param name="useCache">Determines whether to re-issue request or reload last document</param>
        Task<IDocument> BackAsync(bool useCache);

        /// <summary>
        /// Navigate to next document in forward history
        /// </summary>
        /// <returns></returns>
        IDocument Forward();
        
        /// <summary>
        /// Navigate to next document in forward history
        /// </summary>
        /// <param name="useCache"></param>
        /// <returns></returns>
        IDocument Forward(bool useCache);
        
        /// <summary>
        /// Navigate to next document in forward history asynchronously
        /// </summary>
        /// <returns></returns>
        Task<IDocument> ForwardAsync();
        
        /// <summary>
        /// Navigate to next document in forward history asynchronously
        /// </summary>
        /// <param name="useCache"></param>
        /// <returns></returns>
        Task<IDocument> ForwardAsync(bool useCache);
        
        /// <summary>
        /// Max amount of history/Documents to be stored by the browser (-1 for no limit)
        /// </summary>
        int MaxHistorySize { get; set; }

        /// <summary>
        /// Refresh page, re-submits last request
        /// </summary>
        /// <returns></returns>
        IDocument Refresh();
        
        /// <summary>
        /// Refresh page, re-submits last request asynchronously
        /// </summary>
        /// <returns></returns>
        Task<IDocument> RefreshAsync();
        
        /// <summary>
        /// Gets current document from the current request
        /// </summary>
        /// <returns>Current Document</returns>
        IDocument Document { get; }
        
    }
}