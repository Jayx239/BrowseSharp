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
    * Extension of the RestSharp RestClient
    * Generates Documents for each web request.
3. Javascript Engine
    * Used by the browser to scrape css styles from inline styles and linked styles.
4. StyleEngine
    * Used by browser to scrape JavaScript content from inline scripts and externally linked scripts.

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

### Unit Tests:
* With version 0.0.3 unit tests now utilize the [RequestTester](https://github.com/Jayx239/RequestTester) node.js application which can be found on my github page.


### Credits
This application would not have been possible without the contributions by the devolopers of [RestSharp](https://github.com/restsharp/RestSharp), [AngleSharp](https://github.com/AngleSharp/AngleSharp), and [Jint](https://github.com/sebastienros/jint).

Currently Under Development, more to come...

