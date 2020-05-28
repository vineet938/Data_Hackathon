## Configuration

Copy testconfig.json.sample to testconfig.json and update properties.
Here's an example:

```json
{
    "headless": false,
    "debug": true,
    "browser":  "firefox",
    "baseurl": "https://dh2.qa.gs1us.org/gs1usadmin/home",
    "username": "testautogs1user@gmail.com",
    "password": "secret123",
    "apiBaseUrl":  "https://internal-qa-apim.azure-api.net/api/v2",
    "umbrellaAdmin": "194741",
    "apimKey": "8c88c8c111222333a555ababa9996677"
}
```

`headless` (boolean) controls whether to show or hide browser.


`debug` (boolean), if true, shows browser and keeps the browser open after termination.

`browser` (currently "chrome" or "firefox") selects browser to run the test.

`baseurl` is Data Hub admin app URL.

`username` and `password` are user credentials for Data Hub admin app.
The user must be a GS1 US admin.

`apiBaseUrl` is the APIM base URL for the umbrella API.

`apimKey` is a subscription key for the APIM endpoint.


## Running Tests

    dotnet test

If you want to just run specific scenarios, you can use Specflow filter, which can be specified in the `Default.srprofile` file.
