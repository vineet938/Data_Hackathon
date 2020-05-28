This project includes a simple function app and a few proxies.

The purpose of the function app is to "securely" serve SpecFlow HTML test
reports and screenshots that are stored in a blob storage. Since the reports
can contain sensitive information, we want to control the access to them.

The blob storage is protected by its access key. The function app is
configured with Azure AD, requing user authentication.
The function app can access the blob storage thanks to the connection string
to the blob storage we add in function app app settings in azure portal.
Specifically, we add a config variable in the function app settings:
``TestBlobStorageConnectionString``.

The proxies are used to provide more convenient URLs and also to make the
relative links to screenshots work. The function app's default URL is

    http://testrpt.azurewebsites.net/api/GetTestReport?name={name}&extra={extra}

This can be mapped from a shorter URL by the ``Root`` proxy:

    http://testrpt.azurewebsites.net/r/{name}

The ``{extra}`` parameter is the name of a screenshot to be retrieved.

The ``Redirect`` proxy is used to make sure that the short URL ends with a
slash. This is needed for the relative link to the screenshots to work.

The name of the function app "testrpt" is hard-coded in the proxy
settings. Therefore, if this is deployed to a different function app, the
proxies.json file needs to be updated with the new function app name.


