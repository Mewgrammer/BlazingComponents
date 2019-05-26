FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /src
COPY ["Demo/BlazorEssentials.Demo.csproj", "Cloud-In-A-Box/"]
RUN dotnet restore "Demo/BlazorEssentials.Demo.csproj"
COPY . .
WORKDIR "/src/Cloud-In-A-Box"
RUN dotnet build "BlazorEssentials.Demo.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "BlazorEssentials.Demo.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BlazorEssentials.Demo.dll"]