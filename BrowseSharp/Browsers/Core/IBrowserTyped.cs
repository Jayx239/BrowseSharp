using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BrowseSharp.Common;
using BrowseSharp.Common.Html;
using RestSharp;

namespace BrowseSharp.Browsers.Core
{
    /// <summary>
    /// Methods for browser supporting data deserialization
    /// </summary>
    public interface IBrowserTyped
    {
        /// <summary>
        /// Method that makes a get request and creates a document and deserializes response content
        /// </summary>
        /// <param name="uri">Uri for request</param>
        /// <returns>Response Document</returns>
        IDocument<T> Navigate<T>(string uri);

        /// <summary>
        /// Method that makes a get request and creates a document and deserializes response content
        /// </summary>
        /// <param name="uri">Uri for request</param>
        /// <returns>Response Document</returns>
        IDocument<T> Navigate<T>(Uri uri);

        /// <summary>
        /// Method that makes a get request and creates a document and deserializes response content
        /// </summary>
        /// <param name="uri">Uri for request</param>
        /// <param name="headers">Headers to be submitted</param>
        /// <returns>Response Document</returns>
        IDocument<T> Navigate<T>(string uri, Dictionary<string, string> headers);

        /// <summary>
        /// Method that makes a get request and creates a document and deserializes response content
        /// </summary>
        /// <param name="uri">Uri for request</param>
        /// <param name="headers">Headers to be submitted</param>
        /// <returns>Response Document</returns>
        IDocument<T> Navigate<T>(Uri uri, Dictionary<string, string> headers);

        /// <summary>
        /// Method that makes a get request and creates a document and deserializes response content
        /// </summary>
        /// <param name="uri">Uri for request</param>
        /// <param name="headers">Headers to be submitted</param>
        /// <param name="formData">Form data to be sent</param>
        /// <returns>Response Document</returns>
        IDocument<T> Navigate<T>(string uri, Dictionary<string, string> headers, Dictionary<string, string> formData);

        /// <summary>
        /// Method that makes a get request and creates a document and deserializes response content
        /// </summary>
        /// <param name="uri">Uri for request</param>
        /// <param name="headers">Headers to be submitted</param>
        /// <param name="formData">Form data to be sent</param>
        /// <returns>Response Document</returns>
        IDocument<T> Navigate<T>(Uri uri, Dictionary<string, string> headers, Dictionary<string, string> formData);

        /// <summary>
        /// Method that makes an asynchronous get request and creates a document and deserializes response content
        /// </summary>
        /// <param name="uri">Uri for request</param>
        /// <returns>Response Document Task</returns>
        Task<IDocument<T>> NavigateAsync<T>(string uri);

        /// <summary>
        /// Method that makes an asynchronous get request and creates a document and deserializes response content
        /// </summary>
        /// <param name="uri">Uri for request</param>
        /// <returns>Response Document Task</returns>
        Task<IDocument<T>> NavigateAsync<T>(Uri uri);

        /// <summary>
        /// Method that makes an asynchronous get request and creates a document and deserializes response content
        /// </summary>
        /// <param name="uri">Uri for request</param>
        /// <param name="headers">Headers to be submitted</param>
        /// <returns>Response Document Task</returns>
        Task<IDocument<T>> NavigateAsync<T>(string uri, Dictionary<string, string> headers);

        /// <summary>
        /// Method that makes an asynchronous get request and creates a document and deserializes response content
        /// </summary>
        /// <param name="uri">Uri for request</param>
        /// <param name="headers">Headers to be submitted</param>
        /// <returns>Response Document Task</returns>
        Task<IDocument<T>> NavigateAsync<T>(Uri uri, Dictionary<string, string> headers);

        /// <summary>
        /// Method that makes an asynchronous get request and creates a document and deserializes response content
        /// </summary>
        /// <param name="uri">Uri for request</param>
        /// <param name="headers">Headers to be submitted</param>
        /// <param name="formData">Form data to be sent</param>
        /// <returns>Response Document Task</returns>
        Task<IDocument<T>> NavigateAsync<T>(string uri, Dictionary<string, string> headers, Dictionary<string, string> formData);

        /// <summary>
        /// Method that makes an asynchronous get request and creates a document and deserializes response content
        /// </summary>
        /// <param name="uri">Uri for request</param>
        /// <param name="headers">Headers to be submitted</param>
        /// <param name="formData">Form data to be sent</param>
        /// <returns>Response Document Task</returns>
        Task<IDocument<T>> NavigateAsync<T>(Uri uri, Dictionary<string, string> headers, Dictionary<string, string> formData);

        /// <summary>
        /// Method that makes a post request and creates a document and deserializes response content
        /// </summary>
        /// <param name="uri">Uri for request</param>
        /// <returns>Response Document</returns>
        IDocument<T> Submit<T>(string uri);

        /// <summary>
        /// Method that makes a post request and creates a document and deserializes response content
        /// </summary>
        /// <param name="uri">Uri for request</param>
        /// <returns>Response Document</returns>
        IDocument<T> Submit<T>(Uri uri);

        /// <summary>
        /// Method that makes a post request and creates a document and deserializes response content
        /// </summary>
        /// <param name="uri">Uri for request</param>
        /// <param name="formData">Form data to be sent</param>
        /// <returns>Response Document</returns>
        IDocument<T> Submit<T>(string uri, Dictionary<string, string> formData);

        /// <summary>
        /// Method that makes a post request and creates a document and deserializes response content
        /// </summary>
        /// <param name="uri">Uri for request</param>
        /// <param name="formData">Form data to be sent</param>
        /// <returns>Response Document</returns>
        IDocument<T> Submit<T>(Uri uri, Dictionary<string, string> formData);

        /// <summary>
        /// Method that makes a post request and creates a document and deserializes response content
        /// </summary>
        /// <param name="uri">Uri for request</param>
        /// <param name="formData">Form data to be sent</param>
        /// <param name="headers">Headers to be submitted</param>
        /// <returns>Response Document</returns>
        IDocument<T> Submit<T>(string uri, Dictionary<string, string> formData, Dictionary<string, string> headers);

        /// <summary>
        /// Method that makes a post request and creates a document and deserializes response content
        /// </summary>
        /// <param name="uri">Uri for request</param>
        /// <param name="formData">Form data to be sent</param>
        /// <param name="headers">Headers to be submitted</param>
        /// <returns>Response Document</returns>
        IDocument<T> Submit<T>(Uri uri, Dictionary<string, string> formData, Dictionary<string, string> headers);

        /// <summary>
        /// Submits form and deserializes response content
        /// </summary>
        /// <param name="form">Form to be submitted</param>
        /// <returns>Response Document</returns>
        IDocument<T> SubmitForm<T>(Form form);

        /// <summary>
        /// Submits form and deserializes response content
        /// </summary>
        /// <param name="form">Form to be submitted</param>
        /// <param name="Headers"></param>
        /// <returns>Response Document</returns>
        IDocument<T> SubmitForm<T>(Form form, Dictionary<string, string> Headers);

        /// <summary>
        /// Method that makes an asynchronous post request and creates a document and deserializes response content
        /// </summary>
        /// <param name="uri">Uri for request</param>
        /// <returns>Response Document Task</returns>
        Task<IDocument<T>> SubmitAsync<T>(string uri);

        /// <summary>
        /// Method that makes an asynchronous post request and creates a document and deserializes response content
        /// </summary>
        /// <param name="uri">Uri for request</param>
        /// <returns>Response Document Task</returns>
        Task<IDocument<T>> SubmitAsync<T>(Uri uri);

        /// <summary>
        /// Method that makes an asynchronous post request and creates a document and deserializes response content
        /// </summary>
        /// <param name="uri">Uri for request</param>
        /// <param name="formData">Form data to be sent</param>
        /// <returns>Response Document Task</returns>
        Task<IDocument<T>> SubmitAsync<T>(string uri, Dictionary<string, string> formData);

        /// <summary>
        /// Method that makes an asynchronous post request and creates a document and deserializes response content
        /// </summary>
        /// <param name="uri">Uri for request</param>
        /// <param name="formData">Form data to be sent</param>
        /// <returns>Response Document Task</returns>
        Task<IDocument<T>> SubmitAsync<T>(Uri uri, Dictionary<string, string> formData);

        /// <summary>
        /// Method that makes an asynchronous post request and creates a document and deserializes response content
        /// </summary>
        /// <param name="uri">Uri for request</param>
        /// <param name="formData">Form data to be sent</param>
        /// <param name="headers">Headers to be submitted</param>
        /// <returns>Response Document Task</returns>
        Task<IDocument<T>> SubmitAsync<T>(string uri, Dictionary<string, string> formData,
            Dictionary<string, string> headers);

        /// <summary>
        /// Method that makes an asynchronous post request and creates a document and deserializes response content
        /// </summary>
        /// <param name="uri">Uri for request</param>
        /// <param name="formData">Form data to be sent</param>
        /// <param name="headers">Headers to be submitted</param>
        /// <returns>Response Document Task</returns>
        Task<IDocument<T>> SubmitAsync<T>(Uri uri, Dictionary<string, string> formData, Dictionary<string, string> headers);

        /// <summary>
        /// Submits a form asynchronously and deserializes response content
        /// </summary>
        /// <param name="form">Form to be submitted</param>
        /// <returns>Response Document Task</returns>
        Task<IDocument<T>> SubmitFormAsync<T>(Form form);

        /// <summary>
        /// Submits a form asynchronously and deserializes response content
        /// </summary>
        /// <param name="form">Form to be submitted</param>
        /// <param name="headers">Headers to be submitted</param>
        /// <returns>Response Document Task</returns>
        Task<IDocument<T>> SubmitFormAsync<T>(Form form, Dictionary<string, string> headers);

        /// <summary>
        /// Gets current document from the current request as type
        /// </summary>
        /// <returns>Current Typed Document</returns>
        IDocument<T> DocumentTyped<T>();
    }
}
