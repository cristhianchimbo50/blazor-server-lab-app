# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar solución y proyecto
COPY LabWebAppBlazor/LabWebAppBlazor.csproj LabWebAppBlazor/
COPY LabWebAppBlazor.sln ./

# Restaurar dependencias
RUN dotnet restore LabWebAppBlazor.sln

# Copiar todo y compilar
COPY . .
WORKDIR /src/LabWebAppBlazor
RUN dotnet publish -c Release -o /app/out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "LabWebAppBlazor.dll"]