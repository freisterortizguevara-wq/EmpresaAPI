# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /source

# Copiar el archivo del proyecto y restaurar
COPY *.csproj .
RUN dotnet restore

# Copiar el resto del código y publicar
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .

# Configurar el puerto para Render
ENV ASPNETCORE_URLS=http://+:80

# Punto de entrada
ENTRYPOINT ["dotnet", "EmpresaAPI.dll"]
