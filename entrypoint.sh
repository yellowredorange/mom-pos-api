#!/bin/bash
sed -i 's|${DB_PASSWORD}|'"$DB_PASSWORD"'|g' /app/appsettings.Production.json
dotnet MomPosApi.dll