[![Build status](https://michaelmew.visualstudio.com/BlazorEssentials/_apis/build/status/Build%20and%20push%20to%20NuGet)](https://michaelmew.visualstudio.com/BlazorEssentials/_build/latest?definitionId=7)

[![Docker Container Build status](https://michaelmew.visualstudio.com/BlazorEssentials/_apis/build/status/BlazorEssentials-Docker%20container-CI)](https://michaelmew.visualstudio.com/BlazorEssentials/_build/latest?definitionId=8)

## Contents
This Project contains a Component Library and Basic Authentication for Blazor. Components are styled using Bootstrap 4. As server-side Blazor does not support stylesheets in component libraries you will have to install them yourself -> Tutorial is provided below.

There are 2 Packages available on Nuget.org

- [BlazorEssentials.Authentication](https://www.nuget.org/packages/BlazorEssentials.Authentication/)
- [BlazorEssentials.ComponentLib](https://www.nuget.org/packages/BlazorEssentials.ComponentLib/)

### BlazorEssentials.Authentication
This Package includes support for basic authentication in Blazor.
- User Controller for basic Authentication
- UserStateProvider for use in Components
- Components for Login & Register
- BasicUser Service to test Authentication with Users stored in Memory

### BlazorEssentials.ComponentLib
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
- If you want to use Toasts download [toastr.css](https://github.com/CodeSeven/toastr/tree/master/build)
- If you want to use animations with the Expandable Container download [animate.css](https://daneden.github.io/animate.css/)
- If you want to use icons we recommend [fontawesome](https://fontawesome.com/start)
- Download [Bootstrap 4](https://getbootstrap.com/docs/4.0/getting-started/introduction/)
- Install the NuGetPackages [BlazorEssentials.ComponentLib](https://www.nuget.org/packages/BlazorEssentials.ComponentLib/) and optional [BlazorEssentials.Authentication](https://www.nuget.org/packages/BlazorEssentials.Authentication/) for Basic Authentication support into your Project.

Create a Asp.NetCore 3.0 Project for Blazor and put the downloaded libraries in your wwwroot-Folder and make sure to include them in your _Host.cshtml file.

Your_Host.cshtml should look somewhat like this:
```html
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>My Blazor Project</title>
    <base href="~/" />
    <link href="~/libs/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="~/libs/fontawesome/css/all.min.css" rel="stylesheet" />
    <link href="~/css/toast.css" rel="stylesheet">
    <link href="~/css/animate.css" rel="stylesheet">
    <link href="~/css/site.css" rel="stylesheet" />
</head>
<body>
    <app>@(await Html.RenderComponentAsync<App>())</app>

    <script src="_framework/blazor.server.js"></script>
    <script type="text/javascript" src="libs/jquery/jquery.min.js"></script>
    <script type="text/javascript" src="libs/bootstrap/js/bootstrap.min.js"></script>
</body>
</html>

```



## Credits

This Project uses [Sotsera.Blazor.Toaster](https://github.com/sotsera/sotsera.blazor.toaster/blob/master/README.md) for displaying Toasts

Components are styled using the awesome [Bootstrap 4](https://getbootstrap.com/docs/4.0/getting-started/introduction/) Library

BlazorEssentials.Authentication uses [Bcrypt.Net-Core](https://github.com/neoKushan/BCrypt.Net-Core) to secure Passwords

## License

BlazorEssentials is licensed under [MIT license](http://www.opensource.org/licenses/mit-license.php)