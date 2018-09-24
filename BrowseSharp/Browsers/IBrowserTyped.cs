using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BrowseSharp.History;
using BrowseSharp.Html;
using RestSharp;

namespace BrowseSharp.Browsers.Core
{
    interface IBrowserTyped
    {
        /// <summary>
        /// Execute method that creates a document from an http request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IDocument<T> Execute<T>(IRestRequest request);

        /// <summary>
        /// Execute method that creates a document from an http get request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="httpMethod"></param>
        /// <returns></returns>
        IDocument<T> ExecuteAsGet<T>(IRestRequest request, string httpMethod);

        /// <summary>
        /// Execute method that creates a document from an http post request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="httpMethod"></param>
        /// <returns></returns>
        IDocument<T> ExecuteAsPost<T>(IRestRequest request, string httpMethod);

        /// <summary>
        /// Execute method that creates a document from an asynchronous http request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IDocument<T>> ExecuteTaskAsync<T>(IRestRequest request, CancellationToken token);

        /// <summary>
        /// Execute method that creates a document from an asynchronous http request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IDocument<T>> ExecuteTaskAsync<T>(IRestRequest request);

        /// <summary>
        /// Execute method that creates a document from an asynchronous http get request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IDocument<T>> ExecuteGetTaskAsync<T>(IRestRequest request);

        /// <summary>
        /// Execute method that creates a document from an asynchronous http get request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IDocument<T>> ExecuteGetTaskAsync<T>(IRestRequest request, CancellationToken token);

        /// <summary>
        /// Execute method that creates a document from an asynchronous http post request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IDocument<T>> ExecutePostTaskAsync<T>(IRestRequest request);

        /// <summary>
        /// Execute method that creates a document from an asynchronous http post request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IDocument<T>> ExecutePostTaskAsync<T>(IRestRequest request, CancellationToken token);

        /// <summary>
        /// Method that makes a get request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        IDocument<T> Navigate<T>(string uri);

        /// <summary>
        /// Method that makes a get request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        IDocument<T> Navigate<T>(Uri uri);

        /// <summary>
        /// Method that makes a get request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        IDocument<T> Navigate<T>(string uri, Dictionary<string, string> headers);

        /// <summary>
        /// Method that makes a get request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        IDocument<T> Navigate<T>(Uri uri, Dictionary<string, string> headers);

        /// <summary>
        /// Method that makes a get request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="headers"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        IDocument<T> Navigate<T>(string uri, Dictionary<string, string> headers, Dictionary<string, string> formData);

        /// <summary>
        /// Method that makes a get request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="headers"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        IDocument<T> Navigate<T>(Uri uri, Dictionary<string, string> headers, Dictionary<string, string> formData);

        /// <summary>
        /// Method that makes an asynchronous get request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        Task<IDocument<T>> NavigateAsync<T>(string uri);

        /// <summary>
        /// Method that makes an asynchronous get request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        Task<IDocument<T>> NavigateAsync<T>(Uri uri);

        /// <summary>
        /// Method that makes an asynchronous get request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<IDocument<T>> NavigateAsync<T>(string uri, Dictionary<string, string> headers);

        /// <summary>
        /// Method that makes an asynchronous get request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<IDocument<T>> NavigateAsync<T>(Uri uri, Dictionary<string, string> headers);

        /// <summary>
        /// Method that makes an asynchronous get request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="headers"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        Task<IDocument<T>> NavigateAsync<T>(string uri, Dictionary<string, string> headers, Dictionary<string, string> formData);

        /// <summary>
        /// Method that makes an asynchronous get request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="headers"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        Task<IDocument<T>> NavigateAsync<T>(Uri uri, Dictionary<string, string> headers, Dictionary<string, string> formData);

        /// <summary>
        /// Method that makes a post request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        IDocument<T> Submit<T>(string uri);

        /// <summary>
        /// Method that makes a post request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        IDocument<T> Submit<T>(Uri uri);

        /// <summary>
        /// Method that makes a post request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        IDocument<T> Submit<T>(string uri, Dictionary<string, string> formData);

        /// <summary>
        /// Method that makes a post request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        IDocument<T> Submit<T>(Uri uri, Dictionary<string, string> formData);

        /// <summary>
        /// Method that makes a post request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="formData"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        IDocument<T> Submit<T>(string uri, Dictionary<string, string> formData, Dictionary<string, string> headers);

        /// <summary>
        /// Method that makes a post request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="formData"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        IDocument<T> Submit<T>(Uri uri, Dictionary<string, string> formData, Dictionary<string, string> headers);

        /// <summary>
        /// Submits form
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        IDocument<T> SubmitForm<T>(Form form);

        /// <summary>
        /// Submits form
        /// </summary>
        /// <param name="form"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>
        IDocument<T> SubmitForm<T>(Form form, Dictionary<string, string> Headers);

        /// <summary>
        /// Method that makes an asynchronous post request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        Task<IDocument<T>> SubmitAsync<T>(string uri);

        /// <summary>
        /// Method that makes an asynchronous post request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        Task<IDocument<T>> SubmitAsync<T>(Uri uri);

        /// <summary>
        /// Method that makes an asynchronous post request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        Task<IDocument<T>> SubmitAsync<T>(string uri, Dictionary<string, string> formData);

        /// <summary>
        /// Method that makes an asynchronous post request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        Task<IDocument<T>> SubmitAsync<T>(Uri uri, Dictionary<string, string> formData);

        /// <summary>
        /// Method that makes an asynchronous post request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="formData"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<IDocument<T>> SubmitAsync<T>(string uri, Dictionary<string, string> formData,
            Dictionary<string, string> headers);

        /// <summary>
        /// Method that makes an asynchronous post request and creates a document
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="formData"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<IDocument<T>> SubmitAsync<T>(Uri uri, Dictionary<string, string> formData, Dictionary<string, string> headers);

        /// <summary>
        /// Submits a form asynchronously
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        Task<IDocument<T>> SubmitFormAsync<T>(Form form);

        /// <summary>
        /// Submits a form asynchronously
        /// </summary>
        /// <param name="form"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<IDocument<T>> SubmitFormAsync<T>(Form form, Dictionary<string, string> headers);
        
    }
}
