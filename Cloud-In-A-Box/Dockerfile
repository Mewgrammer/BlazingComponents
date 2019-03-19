FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /src
COPY ["Cloud-In-A-Box/Cloud-In-A-Box.csproj", "Cloud-In-A-Box/"]
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