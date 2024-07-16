FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG TARGETARCH
WORKDIR /source

COPY ["MomPosApi.csproj", "./"]
RUN dotnet restore -a $TARGETARCH "MomPosApi.csproj"

COPY . .
RUN dotnet publish -a $TARGETARCH --no-restore -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 443
EXPOSE 80

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MomPosApi.dll"]