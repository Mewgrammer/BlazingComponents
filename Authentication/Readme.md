
### Build

| Branch | Status                                                                                                                                                                                                                   |
| :----: | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| master | [![Build status](https://dev.azure.com/InsiteMichael/Blazing%20Components/_apis/build/status/Blazing%20Components%20Master%20CI)](https://dev.azure.com/InsiteMichael/Blazing%20Components/_build/latest?definitionId=2) |
|  dev   | [![Build status](https://dev.azure.com/InsiteMichael/Blazing%20Components/_apis/build/status/Blazing%20Components%20DevBuild)](https://dev.azure.com/InsiteMichael/Blazing%20Components/_build/latest?definitionId=3)    |
|        |                                                                                                                                                                                                                          |


# Table of Contents
- [Authentication Package](#Authentication-Package)
  - [Getting Started](#Getting-Started-Authentication)
  - [Imports](#Imports)
  - [Startup.cs](#Startupcs)
  - [App.razor](#Apprazor)
  - [User State](#User-State)
  - [UserController](#UserController)
  - [IUserService](#IUserService)
  - [BasicAuthenticationHandler](#BasicAuthenticationHandler)
- [Credits](#Credits)
- [License](#License)

# NuGet Packages
There is a Packages available on Nuget.org

- [BlazingComponents.Authentication](https://www.nuget.org/packages/BlazingComponents.Authentication/)

## BlazingComponents.Authentication
This Package includes support for basic authentication in Blazor.
- User Controller for basic Authentication
- UserStateProvider for use in Components
- Components for Login & Register
- Basic User Service to test Authentication with Users stored in Memory
  
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

# Credits

Components are styled using the awesome [Bootstrap 4](https://getbootstrap.com/docs/4.0/getting-started/introduction/) Library

BlazingComponents.Authentication uses [Bcrypt.Net-Core](https://github.com/neoKushan/BCrypt.Net-Core) to secure Passwords

# License

BlazingComponents is licensed under [MIT license](http://www.opensource.org/licenses/mit-license.php)


If you find any problems/bugs or bad practices feel free to open a issue.