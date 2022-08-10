FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY ./publish/ /app
ENV TZ=America/Guayaquil
ENV ASPNETCORE_URLS=http://+:5010
ENV ASPNETCORE_ENVIRONMENT Development
EXPOSE 5010

ENTRYPOINT ["dotnet", "/app/Personas.Api.dll"]