## Contents
This Project contains a Component Library and Basic Authentication for Blazor. Components are styled using Bootstrap 4.

There are 2 Packages available on Nuget.org

- [BlazingComponents.Authentication](https://www.nuget.org/packages/BlazingComponents.Authentication/)
- [BlazingComponents.Lib](https://www.nuget.org/packages/BlazingComponents.Lib/)

### BlazingComponents.Authentication
This Package includes support for basic authentication in Blazor.
- User Controller for basic Authentication
- UserStateProvider for use in Components
- Components for Login & Register
- Basic User Service to test Authentication with Users stored in Memory

### BlazingComponents.Lib
This Project contains various Components for Blazor. All Components can be used on Client-side or Server-side blazor.

Included Components
- AutoTable - Automatically generates a Table using reflection
- TableComponent - Generates a table based on Template Parameters
- Expandable Container - Supports Animated Expansion/Collapse of Content
- Gallery - Gallery Component with Data Source
- Nav Bar - Horizontal Navigation
- Nav Menu - Vertical Navigation
- UIList - List Component with Data Source
- UITree - Tree Component with various Eventhandlers and Data Source
- Progressbar - Spinner and various Progressbars
  

## Getting Started
- Install the NuGetPackages [BlazorEssentials.ComponentLib](https://www.nuget.org/packages/BlazorEssentials.ComponentLib/) and optional [BlazorEssentials.Authentication](https://www.nuget.org/packages/BlazorEssentials.Authentication/) for Basic Authentication support into your Project.

Create a Asp.NetCore 3.0 Project for Blazor and edit your _Host.cshtml to include the styles and scripts of the BlazingComponent.Lib project.

Your_Host.cshtml should look somewhat like this:
```html
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Blazing Components Demo</title>
    <base href="~/" />
    <link href="_content/BlazingComponents.Lib/libs/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="_content/BlazingComponents.Lib/libs/fontawesome/css/all.min.css" rel="stylesheet" />
    <link href="_content/BlazingComponents.Lib//css/toast.css" rel="stylesheet">
    <link href="_content/BlazingComponents.Lib/css/site.css" rel="stylesheet" />
</head>
<body>
    <app>@(await Html.RenderComponentAsync<App>())</app>
    <script src="_framework/blazor.server.js"></script>
    <script type="text/javascript" src="_content/BlazingComponents.Lib/libs/jquery/jquery.min.js"></script>
    <script type="text/javascript" src="_content/BlazingComponents.Lib/libs/bootstrap/js/bootstrap.min.js"></script>
</body>
</html>
```



## Credits

This Project uses [Sotsera.Blazor.Toaster](https://github.com/sotsera/sotsera.blazor.toaster/blob/master/README.md) for displaying Toasts

Components are styled using the awesome [Bootstrap 4](https://getbootstrap.com/docs/4.0/getting-started/introduction/) Library

BlazingComponents.Authentication uses [Bcrypt.Net-Core](https://github.com/neoKushan/BCrypt.Net-Core) to secure Passwords

## License

BlazingComponents is licensed under [MIT license](http://www.opensource.org/licenses/mit-license.php)