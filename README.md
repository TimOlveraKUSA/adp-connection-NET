## ADP Marketplace Partners

There are a few pre-requesites that you need to fullfill in order to use this library:
- Replace the certifcates in this library with the ones you recieved from the [CSR Tool](https://apps.adp.com/apps/165104)
- Update the client id and client secret with the ones supplied in your credentials document PDF
- Update endpoints from ```https://iat-api.adp.com``` and ```https://iat-accounts.adp.com``` to  ```https://api.adp.com``` and ```https://accounts.adp.com```.

# ADP Client Connection Library for c#/.NET

The ADP Client Connection Library is intended to simplify and aid the process of authenticating, authorizing and connecting to the ADP Marketplace API Gateway. The Library includes a sample application that can be run out-of-the-box to connect to the ADP Marketplace API **test** gateway.

Clone the repo from Github: This allows you to access the raw source code of the library as well as provides the ability to run the sample application and view the Library documentation

### Version
1.0.2

### Installation

**Clone from Github**

You can either use the links on Github or the command line git instructions below to clone the repo.

```sh
$ git clone https://github.com/adplabs/adp-connection-NET.git
$ cd adp-connection-NET

open the solution in VisualStudio
    adp-connection-NET.sln

run the demo client project ADPClientWebDemo to tryout Authorization Code authentication demo
     and/or
run the demo client project ADPClientDemo to tryout Client Credentials authentication code demo

```

The build instruction should install the dependent packages from NuGet else get the packages from NuGet in the packages folder. If you run into errors you may need to open and run the solution in Visual Studio.

##### Alternative:
*Running the sample app*

Load the solution in Visual Studio and Hit [Ctrl F5] (for Start without Debugging)

You can run the sample app included using the Visual Studio environment or deploy it to your favourite ASP.NET web server and enjoy the ease of developing using the ADP Library.

This starts an HTTP server on port 8889 (this port must be unused to run the sample application). You can point your browser to http://localhost:8889. The sample app allows you to connect to the ADP test API Gateway using the **client_credentials** and **authorization_code** grant types. For the **authorization_code** connection, you will be asked to provide an ADP username (MKPLDEMO) and password (marketplace1). The test using the Authorization Code link will prompt a login and upon a successful login present the basic information about the user logged-in.


***

## Examples
### Create Client Credentials ADP Connection

```c#
using ADPClient;
using System;
namespace ADPClientDemo {
    class Program {
        /// <summary>
        /// Demonstrating ADP Client connection library using a product url to get data
        /// after connecting
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args) {
            // get new connection configuration
            // JSON config object placed in Web.config configuration or
            // set individual config object attributes

            string clientconfig = ADPClientDemo.Properties.Settings.Default.ClientCredentialConfiguration;
            ADPAccessToken token = null;
            if (String.IsNullOrEmpty(clientconfig)) {
                Console.WriteLine("Settings file or default options not available.");
            } else {
                ClientCredentialConfiguration connectionCfg = JSONUtil.Deserialize<ClientCredentialConfiguration>(clientconfig);
                ClientCredentialConnection connection = (ClientCredentialConnection)ADPApiConnectionFactory.createConnection(connectionCfg);
                try {
                    connection.connect();
                    if (connection.isConnectedIndicator()) {
                        token = connection.accessToken;
                        Console.WriteLine("Connected to API end point");
                        Console.WriteLine("Token:  ");
                        Console.WriteLine("         AccessToken: {0} ", token.AccessToken);
                        Console.WriteLine("         TokenType: {0} ", token.TokenType);
                        Console.WriteLine("         ExpiresIn: {0} ", token.ExpiresIn);
                        Console.WriteLine("         Scope: {0} ", token.Scope);
                        Console.WriteLine("         ExpiresOn: {0} ", token.ExpiresOn);
                    }
                } catch (Exception e)  { Console.WriteLine(e.Message); }
            }
        }
    }
}
```


## API Documentation ##

Documentation on the individual API calls provided by the library is automatically generated from the library code.

```
Visual Studio build will generate the XML documentation
```

## Tests ##

Nunit tests are available in Nunit test project found in the solution.


Use Visual Studio code analysis feature to check the code coverage..


## Contributing ##

To contribute to the library, please generate a pull request. Before generating the pull request, please insure the following:

1. Appropriate unit tests have been updated or created.
2. Code coverage on the unit tests must be no less than 95%.
3. Your code updates have been fully tested and linted with no errors.
4. Update README.md and API documentation as appropriate.

## License ##

This library is available under the Apache 2 license (http://www.apache.org/licenses/LICENSE-2.0).


