FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine-amd64 AS build
WORKDIR /source

COPY src/. .
RUN dotnet publish -c release -o /app

FROM nginx:1.23.2-alpine
COPY --from=build /app/wwwroot /usr/share/nginx/html