# Dymanic Pages/Componets from Blob storage

## Quick POC loading Pages/Components from some hosted place. In this case Azure blob storage but would quite easily be an authenticated endpoint or an authenticated CDN (best choice for price).


## Overview

1) When you launch the application, it will register certain services that are commonly used throughout the application. For example, BLE, logging service, or authentication service that will be used across multiple pages or features of the application.
2) When you request a page that doesn't exist, the application will attempt to download the necessary `feature` from Azure. If the download is successful, the application will continue with the request and display the page. However, if the download fails, the application will display a 404 error page, indicating that the requested `feature` could not be found.
3) The application will register certain services that are specific to a particular `feature` of the application. These services will be lazy registered, and subsequent requests for the same service will retrieve it from the DI container rather than registering it again. This approach allows the code to be split into smaller, more independent features, making it easier to maintain and update over time.

## Demo 
1) Click the `LOAD FROM AZURE` button on the left nav menu.
2) The first time you load this page you will see a loading icon. I have added an artificial delay to this to prove that it's doing something. 
3) When it's loaded the page it will add the Assembly to the list of assemblys in the [UiAssemblyService](DynamicBlazorUi/Services/UiAssemblyService.cs). The assembly is from the RazorClassLibraryTesting and not compiled into the core app.
4) The next time you load this page the assemly will already be there so it will load instantly.
5) it will inject the TestService that the `host` app has registered and be able to call it without issue
## Where to go from now. 

We would idealy want to load the list of `places` they would need access to and download the routes for each one, save to the device and then do some kind of checks when loading the app to see if they are up to date etc. This would also allow you to keep older versions available if there is an issues and you need to roll back. The idea of a 'shell' app could also be used here although I'm not 100% sure how these would link back into the host services. Would probably need to be in a shared library... So actually yeah that would work. Basically a 'monorepo' with all of the parts in it but you get all of the deployable parts which you can host wherever you need them. 