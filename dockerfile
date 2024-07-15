FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

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
ENTRYPOINT ["dotnet", "MomPosApi.dll"]