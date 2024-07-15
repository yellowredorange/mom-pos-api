FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MomPosApi.csproj", "./"]
RUN dotnet restore "MomPosApi.csproj"
COPY . .
RUN dotnet build "MomPosApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MomPosApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY /etc/ssl/certs/cloudflare.pem /etc/ssl/certs/cloudflare.pem
COPY /etc/ssl/private/cloudflare.key /etc/ssl/private/cloudflare.key
ENTRYPOINT ["dotnet", "MomPosApi.dll"]