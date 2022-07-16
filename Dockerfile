# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY *.sln .
COPY Posterr/*.csproj ./Posterr/
COPY PosterrTests/*.csproj ./PosterrTests/
RUN dotnet restore

COPY . .
RUN dotnet build

# Run unit tests
FROM build AS tests
WORKDIR /app/PosterrTests/
RUN dotnet test

# Copy everything else and build
FROM build AS publish
WORKDIR /app/Posterr
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=publish /app/Posterr/out .
ENTRYPOINT ["dotnet", "Posterr.dll"]