
### Build

| Branch | Status                                                                                                                                                                                                                   |
| :----: | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| master | [![Build status](https://dev.azure.com/InsiteMichael/Blazing%20Components/_apis/build/status/Blazing%20Components%20Master%20CI)](https://dev.azure.com/InsiteMichael/Blazing%20Components/_build/latest?definitionId=2) |
|  dev   | [![Build status](https://dev.azure.com/InsiteMichael/Blazing%20Components/_apis/build/status/Blazing%20Components%20DevBuild)](https://dev.azure.com/InsiteMichael/Blazing%20Components/_build/latest?definitionId=3)    |
|        |                                                                                                                                                                                                                          |


# Table of Contents
1. [Getting Started](#getting-started)
2. [Authentication Library](#Authentication-Package)
   1. [Getting Started](#Getting-Started-Authentication)
   2. [User State](#User-State)
   3. [User Controller](#UserController)
   4. [User Service](#IUserService)
3. [Component Library](#Component-Library-Package)
   1. [Getting Started](#Getting-Started-Component-Lib)
   2. [Auto Table](#Auto-table)
   3. [Blazor Table](#Blazor-table)
   4. [Blazor Gallery](#Blazor-Gallery)
   5. [Blazor List](#Blazor-List)
   6. [Blazor Tree](#Blazor-List)
   7. [Navigation](#Navigation)
   8. [Expandable Container](#Expandable-Container)
   9. [Notifications / Toasts](#Notifications--Toasts)
4. [Credits](#Credits)
5. [License](#License)

# NuGet Packages
There are 2 Packages available on Nuget.org

- [BlazingComponents.Authentication](https://www.nuget.org/packages/BlazingComponents.Authentication/)
- [BlazingComponents.Lib](https://www.nuget.org/packages/BlazingComponents.Lib/)

## BlazingComponents.Authentication
This Package includes support for basic authentication in Blazor.
- User Controller for basic Authentication
- UserStateProvider for use in Components
- Components for Login & Register
- Basic User Service to test Authentication with Users stored in Memory

## BlazingComponents.Lib
This Project contains various Components for Blazor. All Components can be used on Client-side or Server-side blazor.

Included Components
- AutoTable - Automatically generates a Table using reflection
- BlazorTable - Generates a table based on Template Parameters
- Expandable Container - Supports Animated Expansion/Collapse of Content
- Gallery - Gallery Component with Data Source
- Nav Bar - Horizontal Navigation
- Nav Menu - Vertical Navigation
- UIList - List Component with Data Source
- UITree - Tree Component with various Eventhandlers and Data Source
- Progressbar - Spinner and various Progressbars
  
# Authentication Package

## Getting Started (Authentication)

Download the NuGet Package [BlazingComponents.Authentication](https://www.nuget.org/packages/BlazingComponents.Authentication/)

### Imports
Namespaces for Imports:
- BlazingComponents.Authentication.Areas.Components
- BlazingComponents.Authentication.Services
- BlazingComponents.Authentication.Models
- BlazingComponents.Authentication.Interfaces

### Startup.cs
``` c#
        public void ConfigureServices(IServiceCollection services)
        {
            //...
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null)
                .AddCookie();
            services.AddAuthorization();
            services.AddControllers()
                .AddApplicationPart(typeof(UserController).GetTypeInfo().Assembly)
                .AddControllersAsServices();
            services.AddSingleton<IUserService, BasicUserService>();

            services.AddScoped(s =>
            {
                // Creating the URI helper needs to wait until the JS Runtime is initialized, so defer it.
                var uriHelper = s.GetRequiredService<IUriHelper>();
                return new HttpClient
                {
                    BaseAddress = new Uri(uriHelper.GetBaseUri())
                };
            });
            //...
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //...
            app.UseAuthentication();
            app.UseAuthorization();
            //...
        }
```

### App.razor
``` html

    <UserStateProvider>
        <Router AppAssembly="typeof(Startup).Assembly" />
    </UserStateProvider>
```

### User State
``` c#
public class UserState
{
    public string Id { get; set; }
    public string DisplayName { get; set; }
    public bool IsLoggedIn { get; set; }
    public EUserRole Role { get; set; }
    public IDictionary<string, object> UserData { get; set; }
}
```


### UserController
The User Controller will bind to <baseurl>/user, <baseurl>/login, and <baseurl>/register. 
The UserStateProvider will call these Controller-functions to perform logged-in state-check, login and registration.
When replacing the User Controller, make sure the URL stays the same and return a UserState object as it is currently hard coded into the UserStateProvider.
``` c#
[HttpGet("user")]
public ActionResult<UserState> GetUser();
[HttpGet("login")]
public async Task<ActionResult<UserState>> Login();
[HttpPost("register")]
public async Task<ActionResult<UserState>> Register(UserCredentials userCredentials);
[HttpPut("logout")]
public async Task<ActionResult<UserState>> SignOut();
```


### IUserService
Feel free to replace the BasicUserService with your own implementation of IUserService.
``` c#
public interface IUserService
{
    Task<UserState> RegisterAsync(UserCredentials credentials);
    Task<UserState> LoginAsync(UserCredentials credentials);

}
```
### BasicAuthenticationHandler
The BasicAuthenticationHandler receives a instance of IUserService via Dependency Injection.


# Component Library Package

## Getting Started (Component Lib)

Download the NuGet Package [BlazingComponents.Lib](https://www.nuget.org/packages/BlazingComponents.Lib/)

### Imports
Namespaces for Imports:
- BlazingComponents.Lib.Areas.Components
- BlazingComponents.Lib.Services
- BlazingComponents.Lib.Models

### _Host.cshtml
Your _Host.cshtml should look somewhat like this:
```html
    <!DOCTYPE html>
    <html>
        <head>
            <meta charset="utf-8" />
            <meta name="viewport" content="width=device-width, initial-scale=1.0" />
            <title>Your Title Here</title>
            <base href="~/" />
            <link href="_content/BlazingComponents.Lib/libs/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
            <link href="_content/BlazingComponents.Lib/libs/fontawesome/css/all.min.css" rel="stylesheet" />
            <link href="_content/BlazingComponents.Lib/css/toast.css" rel="stylesheet">
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

### App.razor
``` html
    <Router AppAssembly="typeof(Startup).Assembly" />
    <ToastContainer />
```

### Startup.cs
Edit your Startup.cs like this:
``` c#
public void ConfigureServices(IServiceCollection services)
{
    //...
    services.AddComponentLib();
    services.AddScoped(s =>
    {
        // Creating the URI helper needs to wait until the JS Runtime is initialized, so defer it.
        var uriHelper = s.GetRequiredService<IUriHelper>();
        return new HttpClient
        {
            BaseAddress = new Uri(uriHelper.GetBaseUri())
        };
    });
    //...
}

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    //...
    app.UseAuthentication();
    app.UseAuthorization();
    //...
}
```

## Auto Table

#### Parameters

|           Name          	| Type                          	| Description                                                                           	|   	|
|:-----------------------:	|-------------------------------	|---------------------------------------------------------------------------------------	|---	|
| ItemKeyDelegate         	| Func<T, object>               	| Delegate to pass to the @key directive for each item in a row                         	|   	|
| ExpandedItemKeyDelegate 	| Func<T, object>               	| Delegate to pass to the @key directive for each item in the expanded content of a Row 	|   	|
| TableClass              	| string                        	| CSS-class for the table element                                                       	|   	|
| SelectedClass           	| string                        	| CSS-class for the row when it is in the selected state                                	|   	|
| Properties              	| IEnumerable<string>           	| List the properties to use for column-generation (default: all properties are used)   	|   	|
| ExpandedRowTemplate     	| RenderFragment<T>             	| Render Fragment for table expanded row content                                        	|   	|
| Items                   	| IReadOnlyList<T>              	| Items to be used for table-row generation                                             	|   	|
| Selectable              	| bool                          	| Determines whether rows should be selectable                                          	|   	|
| Expandable              	| bool                          	| Determines whether rows should be expandable                                          	|   	|
| MultiSelect             	| bool                          	| Determines whether multiple rows should be selectable                                 	|   	|
| MultiExpand             	| bool                          	| Determines whether multiple rows should be expandable                                 	|   	|
| SelectedItems           	| IList<T>                        	| The selected items                                                                    	|   	|
| ExpandedItems           	| IList<T>                      	| The expanded items                                                                    	|   	|
| OnSelect                	| EventCallback<T>              	| emitted when a row is selected                                                   	|   	|
| OnExpand                	| EventCallback<T>              	| emitted when a row is expanded                                                   	|   	|
| OnSelectMany            	| EventCallback<IEnumerable<T>> 	| emitted when a row or multiple rows are selected                                 	|   	|
| OnExpandMany            	| EventCallback<IEnumerable<T>> 	| emitted when a row or multiple rows is expanded                                  	|   	|

### Example

```html
<AutoTable Items="Persons" Selectable="true" MultiSelect="false">
    <ExpandedRowTemplate Context="person">
        <td colspan="6">
            @($"{person?.FirstName} {person.LastName} born on {person?.BirthDate}")
        </td>
    </ExpandedRowTemplate>
</AutoTable>
```


## Blazor Table

### Parameters
|           Name          	| Type                          	| Description                                                                           	|   	|
|:-----------------------:	|-------------------------------	|---------------------------------------------------------------------------------------	|---	|
| ItemKeyDelegate         	| Func<T, object>               	| Delegate to pass to the @key directive for each item in a row                         	|   	|
| ExpandedItemKeyDelegate 	| Func<T, object>               	| Delegate to pass to the @key directive for each item in the expanded content of a Row 	|   	|
| TableClass              	| string                        	| CSS-class for the table element                                                       	|   	|
| SelectedClass           	| string                        	| CSS-class for the row when it is in the selected state                                	|   	|
| TableHeader     	        | RenderFragment<T>             	| Render Fragment for table header                                                       	|   	|
| RowTemplate           	| RenderFragment<T>             	| Render Fragment for table row content                                                    	|   	|
| ExpandedRowTemplate     	| RenderFragment<T>             	| Render Fragment for table expanded row content                                        	|   	|
| TableFooter            	| RenderFragment<T>             	| Render Fragment for table footer                                                         	|   	|
| Items                   	| IReadOnlyList<T>              	| Items to be used for table-row generation                                             	|   	|
| Selectable              	| bool                          	| Determines whether rows should be selectable                                          	|   	|
| Expandable              	| bool                          	| Determines whether rows should be expandable                                          	|   	|
| MultiSelect             	| bool                          	| Determines whether multiple rows should be selectable                                 	|   	|
| MultiExpand             	| bool                          	| Determines whether multiple rows should be expandable                                 	|   	|
| UsePagination            	| bool                          	| Determines whether the table should have pagination                                    	|   	|
| ItemsPerPageOptions      	| bool                          	| The available options for items per page in the pagination                               	|   	|
| SelectedItems           	| IList<T>                  	    | The selected items                                                                    	|   	|
| ExpandedItems           	| IList<T>                      	| The expanded items                                                                    	|   	|
| OnSelect                	| EventCallback<T>              	| emitted when a row is selected                                                   	|   	|
| OnExpand                	| EventCallback<T>              	| emitted when a row is expanded                                                   	|   	|
| OnSelectMany            	| EventCallback<IEnumerable<T>> 	| emitted when a row or multiple rows are selected                                 	|   	|
| OnExpandMany            	| EventCallback<IEnumerable<T>> 	| emitted when a row or multiple rows is expanded                                  	|   	|
|                         	|                               	|                                


### Example

```html
<BlazorTable @ref="_tableComponent" Items="Persons" UsePagination="true" Expandable="true" Selectable="true" MultiSelect="true" MultiExpand="false">
        <TableHeader>
            <th></th>
            <th>ID</th>
            <th>FirstName</th>
            <th>LastName</th>
            <th>BirthDate</th>
        </TableHeader>
        <RowTemplate Context="person">
            <td>@person.Id</td>
            <td>@person.FirstName</td>
            <td>@person.LastName</td>
            <td>@person.BirthDate</td>
        </RowTemplate>
        <ExpandedRowTemplate Context="person">
            <td colspan="6">
                <table class="table">
                    <thead>
                        <tr>
                            <th>ToString</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                @person
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </ExpandedRowTemplate>
    </BlazorTable>
```

## Blazor Gallery

### Gallery Item Model
``` c#
    public class GalleryItem<T>
    {
        public string ImageSource { get; set; }
        public T Data { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
    }
```
### Parameters
|           Name          	| Type                          	| Description                                                                           	|   	|
|:-----------------------:	|-------------------------------	|---------------------------------------------------------------------------------------	|---	|
| Items                   	| List<GalleryItem<T>>             	| Gallery-Items to be used for the gallery                                                	|   	|
| OnSelect                	| EventCallback<T>              	| emitted when a gallery-item is selected                                                  	|   	|
|                         	|                               	|                                

### Example
``` html
    <BlazorGallery @ref="_gallery" Items="GalleryItems" />
```
``` c#
        public GalleryItem<string> CurrentItem { get; set; }

        public List<GalleryItem<string>> GalleryItems { get; set; } = new List<GalleryItem<string>>
        {
            new GalleryItem<string> { Data = "Hello I am item 1", ImageSource = "http://wowslider.com/sliders/demo-93/data1/images/sunset.jpg", Subtitle = "Subtile of Item 1", Title = "Item 1"},
            new GalleryItem<string> { Data = "Hello I am item 2", ImageSource = "http://wowslider.com/sliders/demo-93/data1/images/sunset.jpg", Subtitle = "Subtile of Item 2", Title = "Item 2"},
            new GalleryItem<string> { Data = "Hello I am item 3", ImageSource = "http://wowslider.com/sliders/demo-93/data1/images/sunset.jpg", Subtitle = "Subtile of Item 3", Title = "Item 3"}
        };
```


## Blazor List

### Parameters
|           Name          	| Type                          	| Description                                                                           	|   	|
|:-----------------------:	|-------------------------------	|---------------------------------------------------------------------------------------	|---	|
| ItemKeyDelegate         	| Func<T, object>               	| Delegate to pass to the @key directive for each item in the list                         	|   	|
| ExpandedItemKeyDelegate 	| Func<T, object>               	| Delegate to pass to the @key directive for each item in the expanded content of a List Entry 	|   	|
| CssClass              	| string                        	| CSS-class for the list (default: "list-group")                                                                   	|   	|
| ItemCssClass           	| string                        	| CSS-class for the list items (default: "list-group")                                                             	|   	|
| SelectedCssClass  	    | string                         	| CSS-class for the items when they are in the selected state  (default: "list-group-item active")                                               	|   	|
| ItemTemplate           	| RenderFragment<T>             	| Render Fragment for list item content                                                    	|   	|
| ExpandedItemTemplate     	| RenderFragment<T>             	| Render Fragment for expanded list item content                                        	|   	|
| Items                   	| IReadOnlyList<T>              	| Items to be used for table-row generation                                             	|   	|
| Selectable              	| bool                          	| Determines whether items should be selectable                                          	|   	|
| Expandable              	| bool                          	| Determines whether items should be expandable                                          	|   	|
| MultiSelect             	| bool                          	| Determines whether multiple items should be selectable                                 	|   	|
| MultiExpand             	| bool                          	| Determines whether multiple items should be expandable                                 	|   	|
| SelectedItems           	| IList<T>                  	    | The selected items                                                                    	|   	|
| ExpandedItems           	| IList<T>                      	| The expanded items                                                                    	|   	|
| OnSelect                	| EventCallback<T>              	| emitted when a item is selected                                                   	|   	|
| OnExpand                	| EventCallback<T>              	| emitted when a item is expanded                                                   	|   	|
| OnSelectMany            	| EventCallback<IEnumerable<T>> 	| emitted when a item or multiple items are selected                                 	|   	|
| OnExpandMany            	| EventCallback<IEnumerable<T>> 	| emitted when a item or multiple items is expanded                                  	|   	|
|                         	|                               	|                                


### Example

```html
<BlazorList @ref="_listRef" Items="@ListItems" Selectable="true" MultiSelect="false" Expandable="true" MultiExpand="false">
    <ItemTemplate Context="item">
        <span>@item.FirstName  @item.LastName , @item.BirthDate</span>
        <button class="btn btn-outline-dark" @onclick="@( () => ExpandItem(item))">Expand</button>
    </ItemTemplate>
    <ExpandedItemTemplate Context="item">
        <ul class="list-group bg-dark text-white">
            <li class="list-group-item list-group-item-dark text-white bg-dark">
                <span>@item.ToString()</span>
            </li>
        </ul>
    </ExpandedItemTemplate>
</BlazorList>
```


## Blazor Tree

### Node Model Class

```c#
public class BlazorTreeNode<T>
    {
        public int Id { get; set; }
        public T Data { get; set; }
        public BlazorTreeNode<T> Parent { get; set; }
        public List<BlazorTreeNode<T>> Children { get; set; } = new List<BlazorTreeNode<T>>();
        public int Deep { get; set; }
        public string Text { get; set; }
        public bool IsExpanded { get; set; } = false;
        public bool IsSelected { get; set; } = false;
        public bool ChildrenLoaded { get; set; } = false;
        public bool IsVisible { get; set; } = true;
    }
```

### Parameters
|           Name          	| Type                          	| Description                                                                           	|   	|
|:-----------------------:	|-------------------------------	|---------------------------------------------------------------------------------------	|---	|
| NodeKeyDelegate         	| Func<BlazorTreeNode<T>, object>               	| Delegate to pass to the @key directive for each node                         	|   	|
| LazyLoadNodesAsyncDelegate 	|Func<int?, Task<List<BlazorTreeNode<T>>>>              	| Delegate for lazy loading nodes asynchronously on init 	|   	|
| Nodes                   	| List<BlazorTreeNode<T>>             	| List of nodes                                             	|   	|
| OnSelect                	| EventCallback<BlazorTreeNode<T>>              	| emitted when a node is selected                                                   	|   	|
| OnExpand                	| EventCallback<BlazorTreeNode<T>>              	| emitted when a node is expanded                                                   	|   	|
| OnCollapse            	| EventCallback<BlazorTreeNode<T>> 	| emitted when a node is collapsed                                 	|   	|
|                         	|                               	|                                


### Example

```html
<BlazorTree @ref="_treeRef"  Nodes="@_nodes">
</BlazorTree>
```

```c#
public List<BlazorTreeNode<string>> _nodes = new List<BlazorTreeNode<string>>(){
    new BlazorTreeNode<string>()
    {
        Text = "Node 1", Children = new List<BlazorTreeNode<string>>()
    {
            new BlazorTreeNode<string>() { Text = "ChildNode 1"},
            new BlazorTreeNode<string>() { Text = "ChildNode 2"},
            new BlazorTreeNode<string>() { Text = "ChildNode 3"},
            new BlazorTreeNode<string>() {
                Text = "ChildNode 4", Children = new List<BlazorTreeNode<string>>()
            {
                    new BlazorTreeNode<string>() { Text = "Nested ChildNode 1"},
                    new BlazorTreeNode<string>() { Text = "Nested ChildNode 2"},
                    new BlazorTreeNode<string>() { Text = "Nested ChildNode 3"},
                }
            },
        }
    }
};
```

## Blazor Progress

### Progress Types

```c#
public enum EProgressType
{
    Spinner, 
    Progress,
    Animated
}
```

### Parameters
|           Name          	| Type                          	| Description                                                                           	|   	|
|:-----------------------:	|-------------------------------	|---------------------------------------------------------------------------------------	|---	|
| ProgressType          	| EProgressType                     | Progress Type (Spinner/Bar)                                                               |   	|
| ProgressValue 	        | int              	                | Progress percentage between 0 and 100 	                                                |   	|

### Example

```html
    <BlazorProgress ProgressType="@(EProgressType.Animated)" ProgressValue="@Progress"></BlazorProgress>
```

```c#
protected async Task ProgressForward()
{
    for (int i = 0; i <= 100; i += 10)
    {
        Progress = i;
        await Task.Delay(300);
        StateHasChanged();
    }
}
```


## Navigation

### Nav-Link Model

```c#
public class NavLinkItem
{
    public string DisplayName { get; set; } = "NavLink";
    public string Href { get; set; } = "";
    public string Icon { get; set; } = "fas fa-link"; // https://fontawesome.com/icons?d=gallery
    public NavLinkMatch Match { get; set; } = NavLinkMatch.All;
}
```

### Horizontal Navigation / Navbar

#### Parameters
|           Name          	| Type                          	| Description                                                                           	|   	|
|:-----------------------:	|-------------------------------	|---------------------------------------------------------------------------------------	|---	|
| Title                   	| string                            | Title                                                                                     |   	|
| NavLinks 	                | List<NavLinkItem>	                | Nav-Links to be displayed              	                                                |   	|

#### Example

```html
    <NavBar Title="Blazing Components" NavLinks="NavLinks" />
```

### Vertical Navigation / NavMenu

#### Parameters
|           Name          	| Type                          	| Description                                                                           	|   	|
|:-----------------------:	|-------------------------------	|---------------------------------------------------------------------------------------	|---	|
| NavLinks 	                | List<NavLinkItem>	                | Nav-Links to be displayed              	                                                |   	|

#### Example

```html
    <NavMenu NavLinks="NavLinks" />
```


## Expandable Container

### Parameters
|           Name          	| Type                          	| Description                                                                           	|   	|
|:-----------------------:	|-------------------------------	|---------------------------------------------------------------------------------------	|---	|
| AnimateExpandClass        | string                            | css class for expansion                                                               |   	|
| AnimateCollapseClass 	    | string                            | css class for collapse 	                                                |   	|
| Expanded          	    | bool                              | Expanded-state                                                               |   	|
| CollapsedContent 	        | RenderFragment                    | RenderFragment for content when container is collapsed 	                                                |   	|
| ExpandedContent          	| RenderFragment                    | RenderFragment for content when container is expanded                                                               |   	|


### Example

```html
<ExpandableContainer @ref="_expandableContainerRef">
    <CollapsedContent>
        <NavMenu NavLinks="@NavLinksIconOnly" />
    </CollapsedContent>
    <ExpandedContent>
        <NavMenu NavLinks="@NavLinks" />
    </ExpandedContent>
</ExpandableContainer>
```

## Notifications / Toasts

Toasts are displayed by using the ToastService.
Inject the ToastService:
 ```c#
  [Inject]
  public IToaster Toaster { get; set; }
  ```
or   `@inject Sotsera.Blazor.Toaster.IToaster Toaster`


### Example
```c#
public ShowToastOnSignalRConnectionStateChange(){
    SignalRClient.ConnectionStateChanged += (state) =>
        {
            if (SignalRClient.IsConnected)
            {
                Toaster.Success("SignalR connection established");
            }
            else if(state == Microsoft.AspNetCore.SignalR.Client.HubConnectionState.Reconnecting)
            {
                Toaster.Warning("SignalR connection unstable");
            }
            else
            {
                Toaster.Error("SignalR disconnected");
            }
        };
}

```


# Credits

This Project uses [Sotsera.Blazor.Toaster](https://github.com/sotsera/sotsera.blazor.toaster/blob/master/README.md) for displaying Toasts

Components are styled using the awesome [Bootstrap 4](https://getbootstrap.com/docs/4.0/getting-started/introduction/) Library

BlazingComponents.Authentication uses [Bcrypt.Net-Core](https://github.com/neoKushan/BCrypt.Net-Core) to secure Passwords

# License

BlazingComponents is licensed under [MIT license](http://www.opensource.org/licenses/mit-license.php)


If you find any problems/bugs or bad practices feel free to open a issue.