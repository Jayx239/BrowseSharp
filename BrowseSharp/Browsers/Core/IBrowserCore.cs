using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BrowseSharp.Browsers.Core
{
    /// <summary>
    /// Browse core methods
    /// </summary>
    public interface IBrowserCore
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
    }
}
