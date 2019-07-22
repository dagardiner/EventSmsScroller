# Event SMS Scroller
This is an application I wrote for a friend's wedding so that people could text advice to them, and it would show up on an automatically updating, infinitely scrolling list that was projected in the reception hall.

The website is ultimately loaded via a URL like https://{AppServiceName}.azurewebsites.net/api/Home?code={HomeFunctionAccessKey}, with the POST parameter &admin loading the page in admin mode, where messages can be hidden.

## Components
Azure App Service (to host Functions)
Azure Storage account (for Table Storage)
Twilio account with a SMS-enabled phone number (to receive texts)

##Configuration
In AzureFunctions/Home, the two html files both contain variables for the name of the AppService hosting the Functions, as well as the keys used to call Azure Functions that need to be populated (the Key is similar to the format jreasdgjKJRkedfjle3wk/KLj==). Both files have the App Service name reference and use the GetData Function key, and the Admin page also uses the HideRecord key.

Within Twilio, you'll need to configure the Messaging Webhook with the URL to the Listener Function (with the key populated properly) - IE https://{YourAppService}.azurewebsites.net/api/Listener?code={ListenerFunctionAccessKey}

The Azure Functions contain some references to Azure Table Storage, which is configured within the Azure Portal.