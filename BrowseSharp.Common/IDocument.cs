using System;
using System.Collections.Generic;
using AngleSharp.Attributes;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using BrowseSharp.Common.Html;
using RestSharp;

namespace BrowseSharp.Common
{
    /// <summary>
    /// BrowseSharp Document containing info about an http request and result
    /// </summary>
    public interface IDocument
    {
        /// <summary>
        /// Response returned from http request
        /// </summary>
        IRestResponse Response { get; set; }

        /// <summary>
        /// Http request object
        /// </summary>
        IRestRequest Request { get; set; }

        /// <summary>
        /// AngleSharp html document scraped from response
        /// </summary>
        IHtmlDocument HtmlDocument { get; set; }

        /// <summary>
        /// Javascripts scraped from http response
        /// </summary>
        List<Javascript.Javascript> Scripts { get; set; }

        /// <summary>
        /// Styles parsed from http response 
        /// </summary>
        List<Style.StyleSheet> Styles { get; set; }

        /// <summary>
        /// Uri of request
        /// </summary>
        Uri RequestUri { get; set; }

        /// <summary>
        /// Forms scraped from response 
        /// </summary>
        List<Form> Forms { get; set; }

        /* AngleSharp wrappers */
        /// <summary>
        /// Returns a list of elements with a given name in the HTML document.
        /// </summary>
        /// <param name="name">
        /// The value of the name attribute of the element.
        /// </param>
        /// <returns>A collection of HTML elements.</returns>
        [DomName("getElementsByName")]
        IHtmlCollection<IElement> GetElementsByName(String name);

        /// <summary>
        /// Returns a set of elements which have all the given class names.
        /// </summary>
        /// <param name="classNames">
        /// A string representing the list of class names to match; class names
        /// are separated by whitespace.
        /// </param>
        /// <returns>A collection of elements.</returns>
        [DomName("getElementsByClassName")]
        IHtmlCollection<IElement> GetElementsByClassName(String classNames);

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
        IHtmlCollection<IElement> GetElementsByTagName(String tagName);

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
        IHtmlCollection<IElement> GetElementsByTagName(String namespaceUri, String tagName);

        /// <summary>
        /// Gets or sets the head element.
        /// </summary>
        [DomName("head")]
        IHtmlHeadElement Head { get; }

        /// <summary>
        /// Gets the body element.
        /// </summary>
        [DomName("body")]
        IHtmlElement Body { get; set; }

        /// <summary>
        /// Gets the child elements.
        /// </summary>
        [DomName("children")]
        IHtmlCollection<IElement> Children { get; }

        /// <summary>
        /// Gets the first child element of this element.
        /// </summary>
        [DomName("firstElementChild")]
        IElement FirstElementChild { get; }

        /// <summary>
        /// Gets the last child element of this element.
        /// </summary>
        [DomName("lastElementChild")]
        IElement LastElementChild { get; }

        /// <summary>
        /// Returns the first element within the document (using depth-first
        /// pre-order traversal of the document's nodes) that matches the
        /// specified group of selectors.
        /// </summary>
        /// <param name="selectors">The group of selectors to use.</param>
        /// <returns>The found element.</returns>
        [DomName("querySelector")]
        IElement QuerySelector(String selectors);

        /// <summary>
        /// Returns a list of the elements within the document (using
        /// depth-first pre-order traversal of the document's nodes) that match
        /// the specified group of selectors.
        /// </summary>
        /// <param name="selectors">The group of selectors to use.</param>
        /// <returns>A non-live NodeList of element objects.</returns>
        [DomName("querySelectorAll")]
        IHtmlCollection<IElement> QuerySelectorAll(String selectors);

        /// <summary>
        /// Response body data
        /// </summary>
        Object Data { get; set; }
    }

    /// <summary>
    /// BrowseSharp Document containing info about an http request and result and a data object of input type
    /// </summary>
    public interface IDocument<T>: IDocument
    {
        /// <summary>
        /// Deserialized body data as type
        /// </summary>
        new T Data { get; set; }
    }
}