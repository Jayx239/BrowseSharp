using System;
using System.Collections.Generic;
using AngleSharp.Attributes;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using BrowseSharp.Common.Html;
using BrowseSharp.Common.Style;
using RestSharp;

namespace BrowseSharp.Common
{
    /// <summary>
    /// Documents that contain data about each http request
    /// </summary>
    public class Document : IDocument
    {
        /// <summary>
        /// Default constructor generating new Document
        /// </summary>
        public Document()
        {
            Scripts = new List<Javascript.Javascript>();
            Styles = new List<StyleSheet>();
        }
        
        /// <summary>
        /// Constructor generating new Document
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        public Document(IRestRequest request, IRestResponse response)
        {
            Request = request;
            Response = response;
            Scripts = new List<Javascript.Javascript>();
            Styles = new List<StyleSheet>();
        }
        
        /// <summary>
        /// Constructor generating new Document
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="htmlDocument"></param>
        public Document(IRestRequest request, IRestResponse response, IHtmlDocument htmlDocument)
        {
            Request = request;
            Response = response;
            HtmlDocument = htmlDocument;
            Scripts = new List<Javascript.Javascript>();
            Styles = new List<StyleSheet>();
            Forms = new List<Form>();
            foreach (IHtmlFormElement form in htmlDocument.Forms)
            {
                Form newForm = new Form(form);
                Forms.Add(newForm);
            }
        }
        
        /// <summary>
        /// Response returned by http request
        /// </summary>
        public IRestResponse Response { get; set; }
        
        /// <summary>
        /// Request made by client
        /// </summary>
        public IRestRequest Request { get; set; }
        
        /// <summary>
        /// Anglesharp document parsed from response
        /// </summary>
        public IHtmlDocument HtmlDocument { get ; set; }
        
        /// <summary>
        /// Javascripts parsed from response
        /// </summary>
        public List<Javascript.Javascript> Scripts { get; set; }
        
        /// <summary>
        /// Stylesheets parsed from response
        /// </summary>
        public List<StyleSheet> Styles { get; set; }

        /// <summary>
        /// Uri of request
        /// </summary>
        public Uri RequestUri { get; set; }

        /// <summary>
        /// Forms scraped from response 
        /// </summary>
        public List<Form> Forms { get; set; }
        
        /* AngleSharp wrappers */
        /// <summary>
        /// Returns a list of elements with a given name in the HTML document.
        /// </summary>
        /// <param name="name">
        /// The value of the name attribute of the element.
        /// </param>
        /// <returns>A collection of HTML elements.</returns>
        [DomName("getElementsByName")]
        public IHtmlCollection<IElement> GetElementsByName(String name)
        {
            return HtmlDocument.GetElementsByName(name);
        }

        /// <summary>
        /// Returns a set of elements which have all the given class names.
        /// </summary>
        /// <param name="classNames">
        /// A string representing the list of class names to match; class names
        /// are separated by whitespace.
        /// </param>
        /// <returns>A collection of elements.</returns>
        [DomName("getElementsByClassName")]
        public IHtmlCollection<IElement> GetElementsByClassName(String classNames)
        {
            return HtmlDocument.GetElementsByClassName(classNames);
        }

        /// <summary>
        /// Returns a NodeList of elements with the given tag name. The
        /// complete document is searched, including the root node.
        /// </summary>
        /// <param name="tagName">
        /// A string representing the name of the elements. The special string
        /// "*" represents all elements.
        /// </param>
        /// <returns>
        /// A collection of elements in the order they appear in the tree.
        /// </returns>
        [DomName("getElementsByTagName")]
        public IHtmlCollection<IElement> GetElementsByTagName(String tagName)
        {
            return HtmlDocument.GetElementsByTagName(tagName);
        }

        /// <summary>
        /// Returns a list of elements with the given tag name belonging to the
        /// given namespace. The complete document is searched, including the
        /// root node.
        /// </summary>
        /// <param name="namespaceUri">
        /// The namespace URI of elements to look for.
        /// </param>
        /// <param name="tagName">
        /// Either the local name of elements to look for or the special value
        /// "*", which matches all elements.
        /// </param>
        /// <returns>
        /// A collection of elements in the order they appear in the tree.
        /// </returns>
        [DomName("getElementsByTagNameNS")]
        public IHtmlCollection<IElement> GetElementsByTagName(String namespaceUri, String tagName)
        {
            return HtmlDocument.GetElementsByTagName(namespaceUri, tagName);
        }
        
        /// <summary>
        /// Gets or sets the head element.
        /// </summary>
        [DomName("head")]
        public IHtmlHeadElement Head
        {
            get { return HtmlDocument.Head; }
        }

        /// <summary>
        /// Gets the body element.
        /// </summary>
        [DomName("body")]
        public IHtmlElement Body
        {
            get { return HtmlDocument.Body;}
            set { HtmlDocument.Body = value; }
        }
        
        /// <summary>
        /// Gets the child elements.
        /// </summary>
        [DomName("children")]
        public IHtmlCollection<IElement> Children
        {
            get { return HtmlDocument.Children; }
        }

        /// <summary>
        /// Gets the first child element of this element.
        /// </summary>
        [DomName("firstElementChild")]
        public IElement FirstElementChild
        {
            get { return HtmlDocument.FirstElementChild; }
        }

        /// <summary>
        /// Gets the last child element of this element.
        /// </summary>
        [DomName("lastElementChild")]
        public IElement LastElementChild
        {
            get { return HtmlDocument.LastElementChild; }
        }

        /// <summary>
        /// Returns the first element within the document (using depth-first
        /// pre-order traversal of the document's nodes) that matches the
        /// specified group of selectors.
        /// </summary>
        /// <param name="selectors">The group of selectors to use.</param>
        /// <returns>The found element.</returns>
        [DomName("querySelector")]
        public IElement QuerySelector(String selectors)
        {
            return HtmlDocument.QuerySelector(selectors);
        }

        /// <summary>
        /// Returns a list of the elements within the document (using
        /// depth-first pre-order traversal of the document's nodes) that match
        /// the specified group of selectors.
        /// </summary>
        /// <param name="selectors">The group of selectors to use.</param>
        /// <returns>A non-live NodeList of element objects.</returns>
        [DomName("querySelectorAll")]
        public IHtmlCollection<IElement> QuerySelectorAll(String selectors)
        {
            return HtmlDocument.QuerySelectorAll(selectors);
        }
        
        /// <summary>
        /// Body data
        /// </summary>
        public object Data { get; set; }
    }
    /// <summary>
    /// Document with typed body
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Document<T> : Document, IDocument<T>
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Document() : base()
        {

        }

        /// <summary>
        /// Secondary Contstructor
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        public Document(IRestRequest request, IRestResponse<T> response) : base(request, response)
        {
            Data = response.Data;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="htmlDocument"></param>
        public Document(IRestRequest request, IRestResponse<T> response, IHtmlDocument htmlDocument) : base(request, response, htmlDocument)
        {
            Data = response.Data;
        }

        /// <summary>
        /// Gets data as type
        /// </summary>
        public new T Data { get; set; }
    }
}