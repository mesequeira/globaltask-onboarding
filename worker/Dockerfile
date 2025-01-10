# Usar una imagen base de .NET
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar los archivos de proyecto con las rutas ajustadas
COPY ["Users.Worker/Users.Worker.csproj", "Users.Worker/"]
COPY ["Users.Worker.Application/Users.Worker.Application.csproj", "Users.Worker.Application/"]
COPY ["Users.Worker.Domain/Users.Worker.Domain.csproj", "Users.Worker.Domain/"]
COPY ["Users.Worker.Infrastructure/Users.Worker.Infrastructure.csproj", "Users.Worker.Infrastructure/"]
COPY ["Users.Worker.Persistence/Users.Worker.Persistence.csproj", "Users.Worker.Persistence/"]

# Restaurar dependencias
RUN dotnet restore "Users.Worker/Users.Worker.csproj"

# Copiar todo el código fuente
COPY . .

# Establecer el directorio de trabajo y construir el proyecto
WORKDIR "/src/Users.Worker"
RUN dotnet build -c Release -o /app/build

# Publicar la aplicación
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Configuración final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Users.Worker.dll"]
