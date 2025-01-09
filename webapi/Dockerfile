# Base stage: .NET runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5000

# Build stage: SDK for building the project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY Users.WebApi/Users.WebApi.csproj ./Users.WebApi/
COPY Users.Application/Users.Application.csproj ./Users.Application/
COPY Users.Domain/Users.Domain.csproj ./Users.Domain/
COPY Users.Infrastructure/Users.Infrastructure.csproj ./Users.Infrastructure/
COPY Users.Persistence/Users.Persistence.csproj ./Users.Persistence/
RUN dotnet restore ./Users.WebApi/Users.WebApi.csproj

# Copy the rest of the code
COPY . .
WORKDIR /src/Users.WebApi
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Users.WebApi.dll"]