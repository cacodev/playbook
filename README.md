# Playbook - Feature Flagging

Ready to start feature flagging? Add Playbook to your project and setup a feature flag playbook today!

Playbook is a simple way to add feature flagging to your ASP.net core project. First, You create a configuration with plays and add players to that play. Then, you can either leverage the services that Playbook provides or call the Playbook api from your client.

## Getting started

You can add a playbook configuration to your `appsettings.json` like so:

``` json
{
    ...
    "Playbook" : {
        "PLAY_1" : {
            "Players": ["USERNAME1","USERNAME2"]
        }
    }
    ...
}
```

Then, register playbook in your startup:

``` c#
// include this using
using Playbook;

// inside startup class
public void ConfigureServices(IServiceCollection services)
{
    // add this to register playbook services
    // Configuration param is required 
    // as it is used to populate the playbook
    services.AddPlaybook(Configuration);
}

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    // add this to your app builder   
    app.UsePlaybook();
}
```