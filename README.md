# BrowseSharp
[BrowseSharp.org](https://browsesharp.org)
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
	* Generates Documents for each web request
    * Type deserialization of response data
    * Simple nagivate and submit methods for get/post requests
    * Browser history for backward forward navigation and refreshing the current document
3. Javascript Engine
	* Used by browser to scrape JavaScript content from inline scripts and externally linked scripts.
4. StyleEngine
 	* Used by the browser to scrape css styles from inline styles and linked styles.
  
### Example Usage
```
static void Main(string[] args)
{
    /* Initialize Browser */
    Browser browser = new Browser();

    /* Navigate to a website */
    IDocument document = browser.Navigate("https://www.browsesharp.org/testsitesforms.html");

    / * Navigate with headers */
    Dictionary<string, string> headers = new Dictionary<string, string>();
    headers.Add("x-csrf-token", "2342342");
    var response = browser.Navigate(RequestTesterRouteUri, headers);

    /* Navigate with data */
    Dictionary<string, string> formData = new Dictionary<string, string>();
    formData.Add("Username","FakeUserName");
    formData.Add("Password", "FakePassword123");
    formData.Add("SecretMessage", "This is a secret message");
    var response = browser.Navigate(RequestTesterRouteUri, headers, formData);



    /* SubmitForm */
    /* Form */
    browser.Navigate("https://www.browsesharp.org/testsitesforms.html");
    Form postForm = browser.Document.Forms[0];
    postForm.SetValue("UserName", "TestUser");
    postForm.SetValue("Password", "TestPassword");
    IDocument postResponse = browser.SubmitForm(postForm);
    
    /* Form as dictionary */
    var response = browser.Submit("https://www.hashemian.com/tools/form-post-tester.php", formData);
    
    /* With headers */
    var response = browser.Submit("https://requesttester.com", formData, headers);


    /* Async calls */
    var response = await browser.NavigateAsync("https://www.browsesharp.org/testsitesforms.html");


    /* Get current document */
    IDocument lastDocument = browser.Document;


    /* Typed Responses 
        Note: Request is a custom class unrelated to this library
    */
    /* Make request that is deserialized into a request object */
    IDocument<Request> response = browser.Navigate<Request>(RequestTesterRouteJsonUri, headers, formData);
    Request request = response.Data; /* Get the Request object from the response */



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

### Project Website
* BrowseSharp now has a website! Check it out now at [www.browsesharp.org](https://www.browsesharp.org)


Currently Under Development, more to come...

