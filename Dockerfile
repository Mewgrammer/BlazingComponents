FROM mcr.microsoft.com/dotnet/core-nightly/aspnet:3.0.0-preview5 AS base
WORKDIR /app
EXPOSE 80	
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core-nightly/sdk:3.0.100-preview5 AS build
WORKDIR /src
COPY ["Demo/BlazingComponents.Demo.csproj", "Demo/"]
COPY ["Components/BlazingComponents.Lib.csproj", "Components/"]
COPY ["Authentication/BlazingComponents.Authentication.csproj", "Authentication/"]
RUN dotnet restore "Demo/BlazingComponents.Demo.csproj"
COPY . .
WORKDIR "/src/Demo"
RUN dotnet build "BlazingComponents.Demo.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "BlazingComponents.Demo.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BlazingComponents.Demo.dll"]
