# Use the official .NET image as the build environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MoviesAPI/Movies.API.csproj", "MoviesAPI/"]
COPY ["Movies.Domain/Movies.Domain.csproj", "Movies.Domain/"]
COPY ["Movies.Infrastructure/Movies.Infrastructure.csproj", "Movies.Infrastructure/"]
COPY ["Movies.Services/Movies.Services.csproj", "Movies.Services/"]
RUN dotnet restore "MoviesAPI/Movies.API.csproj"
COPY . .
WORKDIR "/src/MoviesAPI"
RUN dotnet build "Movies.API.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "Movies.API.csproj" -c Release -o /app/publish

# Create the runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Movies.API.dll"]
