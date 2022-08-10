FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS build-env
WORKDIR /app

COPY ./publish/ /app
ENV ASPNETCORE_URLS=http://+:5003
ENV ASPNETCORE_ENVIRONMENT Development
EXPOSE 5003

ENTRYPOINT ["dotnet", "/app/Catalogo.Api.dll"]