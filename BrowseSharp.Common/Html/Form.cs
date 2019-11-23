using System.Collections.Generic;
using AngleSharp.Html.Dom;

namespace BrowseSharp.Common.Html
{
    /// <summary>
    /// Object containing form attributes
    /// </summary>
    public class Form
    {
        /// <summary>
        /// Initializes form with AngleSharp form element
        /// </summary>
        /// <param name="formElement"></param>
        public Form(IHtmlFormElement formElement)
        {
            HtmlForm = formElement;
            _formControls = formElement.Elements;
            FormValues = GetFormFields();
        }
        
        /// <summary>
        /// AngleSharp Html Form
        /// </summary>
        public IHtmlFormElement HtmlForm { get; set; }
        
        /// <summary>
        /// AngleSharp FormControls object
        /// </summary>
        public IHtmlFormControlsCollection FormControls
        {
            get { return _formControls; }
        }
         
        /// <summary>
        /// Form key value pairs
        /// </summary>
        public Dictionary<string,string> FormValues { get; set; }
        
        /// <summary>
        /// AngleSharp FormControls object
        /// </summary>
        private IHtmlFormControlsCollection _formControls;
        
        /// <summary>
        /// Form method (post,get)
        /// </summary>
        public string Method
        {
            get { return HtmlForm.Method;}
            set { HtmlForm.Method = value; }
        }
        
        /// <summary>
        /// Form action
        /// </summary>
        public string Action
        {
            get { return HtmlForm.Action; }
            set { HtmlForm.Action = value; }
        }
        
        /// <summary>
        /// Gets the form key value pairs with their default values
        /// </summary>
        /// <returns>Form fields with their default values</returns>
        public Dictionary<string,string> GetFormFields()
        {
            Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();
            foreach(var formControl in FormControls)
            {
                var attributes = formControl.Attributes;
                if (attributes != null && attributes["name"] != null && !nameValuePairs.ContainsKey(attributes["name"].Value))
                    nameValuePairs.Add(attributes["name"].Value, attributes["value"]?.Value??"");
            }
            return nameValuePairs;
        }

        /// <summary>
        /// Sets a form value
        /// </summary>
        /// <param name="inputName">Form element name</param>
        /// <param name="inputValue">Form element value</param>
        /// <returns>Boolean indicating if form element was set</returns>
        public bool SetValue(string inputName, string inputValue)
        {
            if (FormValues != null)
            {
                if (FormValues[inputName] != null)
                    FormValues[inputName] = inputValue;
                else
                    FormValues.Add(inputName, inputValue);
                return true;
            }
            else
                return false;
        }
    }
}