FROM microsoft/dotnet:3.0-aspnetcore-runtime-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:3.0-sdk-stretch AS build
WORKDIR /src
COPY ["Cloud-In-A-Box/Cloud-In-A-Box.csproj", "Cloud-In-A-Box/"]
COPY ["Components/Cloud-In-A-Box.Components.csproj", "Components/"]
RUN dotnet restore "Cloud-In-A-Box/Cloud-In-A-Box.csproj"
COPY . .
WORKDIR "/src/Cloud-In-A-Box"
RUN dotnet build "Cloud-In-A-Box.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Cloud-In-A-Box.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Cloud-In-A-Box.dll"]