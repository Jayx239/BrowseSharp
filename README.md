# BrowseSharp
[![NuGet](https://img.shields.io/nuget/v/BrowseSharp.svg)](https://www.nuget.org/packages/BrowseSharp)

A headless browser supporting web navigation, html parsing, and javascript execution.

### Current Features
1. Documents
	* Store Request Data
	* Stores Response Data
	* Stores javascripts
	* Stores css styles
2. Browser
	* Supports asynchronous and synchronous web requests
	* Generates Documents for each web request.
3. Javascript Engine
	* Used by browser to scrape JavaScript content from inline scripts and externally linked scripts.
4. StyleEngine
 	* Used by the browser to scrape css styles from inline styles and linked styles.
### Example Usage
```
static void Main(string[] args)
{
	/* Make request */
    BrowseSharp.Browser browser = new Browser();
    browser.BaseUrl = new Uri("https://www.w3schools.com");
    RestSharp.RestRequest request = new RestSharp.RestRequest(RestSharp.Method.GET);
    RestSharp.IRestResponse response = browser.Execute(request);

    /* Run script - This will be cleaned up! */
    browser.JavascriptEngine.Document = browser.Documents[0].Scripts[2].JavascriptString;
    var result = browser.JavascriptEngine.Execute("w3CodeColor();");

    /* Output */
    Document requestedDocument = browser.Documents[0];
    Console.WriteLine("Scraped Scripts: " + requestedDocument.Scripts.Count);
    Console.WriteLine("Scraped Styles: " + requestedDocument.Styles.Count);
    Console.WriteLine("Begining of document: " + requestedDocument.Response.Content.Substring(0, 60));

}
```
Currently Under Development, more to come...

