FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Users.Worker/Users.Worker.csproj", "Users.Worker/"]
COPY ["Users.Worker.Application/Users.Worker.Application.csproj", "Users.Worker.Application/"]
COPY ["Users.Worker.Domain/Users.Worker.Domain.csproj", "Users.Worker.Domain/"]
RUN dotnet restore "Users.Worker/Users.Worker.csproj"
COPY . .
WORKDIR "/src/Users.Worker"
RUN dotnet build "Users.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Users.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Users.Worker.dll"]
