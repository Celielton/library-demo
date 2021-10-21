FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build-env
WORKDIR /app

#Show .NET version
RUN dotnet --version

# copy csproj then restore
COPY src/*/*.csproj ./

# Build
COPY . ./
RUN dotnet publish -c Release -o out

# Build image
FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine
WORKDIR /app
COPY --from=build-env /app/out .
ENV environment=development
ENTRYPOINT ["dotnet", "library-api.dll"]